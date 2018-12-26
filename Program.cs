using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramBot.DAL;
using TelegramBot.Models;

namespace TelegramBot {
    class Program {
        static ITelegramBotClient botClient;
        static string token = Authentication.token;
        static TelegramBotContext context = new TelegramBotContext();

        static Regex helloRegex = new Regex(@"^hello|hi|hey$", RegexOptions.IgnoreCase);

        static void Main() {
            botClient = new TelegramBotClient(token);
            var me = botClient.GetMeAsync().Result;

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            botClient.StopReceiving();
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e) {
            if (e.Message.Text != null) {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

                string message = e.Message.Text.Trim();
                string reply = "";

                if(helloRegex.Matches(message).Count > 0) {
                    reply = "Hello!";
                } else if(message.StartsWith("/help")) {
                    reply = sendHelp();
                } else if(message.StartsWith("/remove")) {
                    reply = removeItem(message);
                } else if(message.StartsWith("/add")) {
                    reply = addItem(message);
                } else if(message.StartsWith("/get")) {
                    reply = getItems();
                } else if(message.StartsWith("/clear")) {
                    reply = clearItems();
                } else {
                    reply = "Unknown option.";
                }

                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: reply
                );
            }
        }

        static string removeItem(string message) {
            var messageItem = message.Substring(7).TrimStart();
            var itemToRemove = context.Items.FirstOrDefault(x => x.name == messageItem);
            if(itemToRemove != null) {
                context.Remove(itemToRemove);
                context.SaveChanges();
                return $"Removing {itemToRemove.name}.";
            } else {
                return $"Could not remove {messageItem}.";
            }
        }

        static string addItem(string message) {
            var messageItem = message.Substring(4).TrimStart();
            context.Items.Add(new Items {
                name=messageItem
            });

            context.SaveChanges();
            return $"Adding {messageItem}.";
        }

        static string sendHelp() {
            return "Good luck";
        }

        static string clearItems() {
            context.Items.RemoveRange(context.Items);
            context.SaveChanges();
            return "All items have been removed from your list.";
        }

        static string getItems() {
            var toReturn = "";

            if(context.Items.Count() > 0) {
                var allItems = context.Items.Select(x => x).ToList();
                toReturn = "These are the items on your list:\n\n";
                for(var i = 0; i < allItems.Count; i++) {
                    toReturn += "- " + allItems.ElementAt(i).name + "\n";
                }
            } else {
                toReturn = "There are currently no items in your list.";
            }
            return toReturn;
        }
    }
}
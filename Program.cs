using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Microsoft.EntityFrameworkCore;

namespace TelegramBot {
    class Program {
        static ITelegramBotClient botClient;
        static string token = Authentication.token;
        static List<Items> itemList = new List<Items>();

        static void Main() {
        botClient = new TelegramBotClient(token);

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e) {
            if (e.Message.Text != null) {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

                string message = e.Message.Text.Trim();
                string reply = "";

                if(message.StartsWith("/help")) {
                    reply = sendHelp();
                } else if(message.StartsWith("/remove")) {
                    reply = removeItem(message);
                } else if(message.StartsWith("/add")) {
                    reply = addItem(message);
                } else if(message.StartsWith("/get")) {
                    reply = getItems();
                } else if(message.StartsWith("/clear")) {
                    reply = getItems();
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
            var itemToRemove = itemList.Find(x => x.name == messageItem);
            itemList.Remove(itemToRemove);

            return $"Removing {itemToRemove.name}";
        }

        static string addItem(string message) {
            var messageItem = message.Substring(4).TrimStart();
            var item = new Items {
                name = messageItem,
                itemId = 0
            };

            itemList.Add(item);
            return $"Adding {item.name}";
        }

        static string sendHelp() {
            return "Good luck";
        }

        static string clearItems() {
            itemList.Clear();
            return "All items have been removed from the list";
        }

        static string getItems() {
            var toReturn = "";

            if(itemList.Count > 0) {
                toReturn = "These are the items on your list:\n\n";
                for(var i = 0; i < itemList.Count; i++) {
                    toReturn += "*>*" + itemList.ElementAt(i).name + "\n";
                }
            } else {
                toReturn = "There are currently no items in your list";
            }
            return toReturn;
        }
    }
}
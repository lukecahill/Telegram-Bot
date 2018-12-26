using System.ComponentModel.DataAnnotations;

namespace TelegramBot {
    class Items {
        [Key]
        public int itemId { get; set; }
        public string name { get; set; }
    }
}
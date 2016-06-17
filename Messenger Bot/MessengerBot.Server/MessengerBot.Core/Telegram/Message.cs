using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerBot.Core.Telegram
{
    /// <summary>
    /// This object represents a message.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Unique message identifier
        /// </summary>
        public int message_id { get; set; }
        /// <summary>
        /// [Optional] Sender, can be empty for messages sent to channels
        /// </summary>
        public User from { get; set; }
        /// <summary>
        /// Date the message was sent in Unix time
        /// </summary>
        public int date { get; set; }
        /// <summary>
        /// Conversation the message belongs to
        /// </summary>
        public Chat chat { get; set; }
        /// <summary>
        /// [Optional] For forwarded messages, sender of the original message
        /// </summary>
        public User forward_from { get; set; }
        /// <summary>
        /// [Optional] For messages forwarded from a channel, information about the original channel
        /// </summary>
        public Chat forward_from_chat { get; set; }
        /// <summary>
        /// [Optional] For forwarded messages, date the original message was sent in Unix time
        /// </summary>
        public int forward_date { get; set; }
        /// <summary>
        /// [Optional] For replies, the original message. 
        /// Note that the Message object in this field will not contain further 
        /// reply_to_message fields even if it itself is a reply.
        /// </summary>
        public Message reply_to_message { get; set; }
        /// <summary>
        /// [Optional] Date the message was last edited in Unix time
        /// </summary>
        public int edit_date { get; set; }
        /// <summary>
        /// [Optional] For text messages, the actual UTF-8 text of the message, 0-4096 characters.
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// [Optional] For text messages, special entities like usernames, URLs, bot commands, 
        /// etc. that appear in the text
        /// </summary>
        public MessageEntity[] entities { get; set; }
        /// <summary>
        /// [Optional] Message is an audio file, information about the file
        /// </summary>
        public Audio audio { get; set; }
        /// <summary>
        /// [Optional] Message is a general file, information about the file
        /// </summary>
        public Document document { get; set; }
        /// <summary>
        /// [Optional] Message is a photo, available sizes of the photo
        /// </summary>
        public PhotoSize[] photo { get; set; }
        /// <summary>
        /// [Optional] Message is a sticker, information about the sticker
        /// </summary>
        public Sticker sticker { get; set; }
        /// <summary>
        /// [Optional] Message is a video, information about the video
        /// </summary>
        public Video video { get; set; }
        /// <summary>
        /// [Optional] Message is a voice message, information about the file
        /// </summary>
        public Voice voice { get; set; }
        /// <summary>
        /// [Optional] Caption for the document, photo or video, 0-200 characters
        /// </summary>
        public string caption { get; set; }
        /// <summary>
        /// [Optional] Message is a shared contact, information about the contact
        /// </summary>
        public Contact contact { get; set; }
        /// <summary>
        /// [Optional] Message is a shared location, information about the location
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// [Optional] Message is a venue, information about the venue
        /// </summary>
        public Venue venue { get; set; }
        /// <summary>
        /// [Optional] A new member was added to the group, information about them (this member may be the bot itself)
        /// </summary>
        public User new_chat_member { get; set; }
        /// <summary>
        /// [Optional] A member was removed from the group, information about them (this member may be the bot itself)
        /// </summary>
        public User left_chat_member { get; set; }
        /// <summary>
        /// [Optional] A chat title was changed to this value
        /// </summary>
        public string new_chat_title { get; set; }
        /// <summary>
        /// [Optional] A chat photo was change to this value
        /// </summary>
        public PhotoSize[] new_chat_photo { get; set; }
        /// <summary>
        /// [Optional] Service message: the chat photo was deleted
        /// </summary>
        public bool delete_chat_photo { get; set; }
        /// <summary>
        /// [Optional] Service message: the group has been created
        /// </summary>
        public bool group_chat_created { get; set; }
        /// <summary>
        /// [Optional] Service message: the supergroup has been created. 
        /// This field can‘t be received in a message coming through updates, because bot can’t 
        /// be a member of a supergroup when it is created. 
        /// It can only be found in reply_to_message if someone replies to a very first message
        /// in a directly created supergroup.
        /// </summary>
        public bool supergroup_chat_created { get; set; }
        /// <summary>
        /// [Optional] Service message: the channel has been created. This field can‘t be received in a
        /// message coming through updates, because bot can’t be a member of a channel when it is created.
        /// It can only be found in reply_to_message if someone replies to a very first message in a channel.
        /// </summary>
        public bool channel_chat_created { get; set; }
        /// <summary>
        /// [Optional] The group has been migrated to a supergroup with the specified identifier. 
        /// This number may be greater than 32 bits and some programming languages may have 
        /// difficulty/silent defects in interpreting it. But it smaller than 52 bits, 
        /// so a signed 64 bit integer or double-precision float type are safe for storing this identifier.
        /// </summary>
        public int migrate_to_chat_id { get; set; }
        /// <summary>
        /// [Optional]m The supergroup has been migrated from a group with the specified identifier.
        /// This number may be greater than 32 bits and some programming languages may have
        /// difficulty/silent defects in interpreting it. But it smaller than 52 bits, 
        /// so a signed 64 bit integer or double-precision float type are safe for storing this identifier.
        /// </summary>
        public int migrate_from_chat_id { get; set; }
        /// <summary>
        /// [Optional] Specified message was pinned. Note that the Message object in this field
        /// will not contain further reply_to_message fields even if it is itself a reply.
        /// </summary>
        public Message pinned_message { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerBot.Core.Telegram
{
    /// <summary>
    /// This object represent a user's profile pictures.
    /// </summary>
    public class UserProfilePhotos
    {
        /// <summary>
        /// Total number of profile pictures the target user has
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// Requested profile pictures (in up to 4 sizes each)
        /// </summary>
        public PhotoSize[][] photos { get; set; }
    }
}

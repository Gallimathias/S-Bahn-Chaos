using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerBot.Core.Telegram
{
    /// <summary>
    /// This object represents a file ready to be downloaded. 
    /// The file can be downloaded via the link https://api.telegram.org/file/bot<token>/<file_path>.
    /// It is guaranteed that the link will be valid for at least 1 hour.
    /// When the link expires, a new one can be requested by calling getFile.
    /// </summary>
    public class File
    {
        /// <summary>
        /// Unique identifier for this file
        /// </summary>
        public string file_id { get; set; }
        /// <summary>
        /// [Optional] File size, if known
        /// </summary>
        public int file_size { get; set; }
        /// <summary>
        /// [Optional]
        /// File path. Use https://api.telegram.org/file/bot<token>/<file_path> to get the file.
        /// </summary>
        public string file_path { get; set; }
    }
}

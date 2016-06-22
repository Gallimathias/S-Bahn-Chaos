using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerBot.Core
{
    public class CommandHandler<TArguments, TResult>
    {
        private Dictionary<string, Func<TArguments, TResult>> delegates { get; set; }

        public CommandHandler()
        {
            delegates = new Dictionary<string, Func<TArguments, TResult>>();
        }

        public Func<TArguments, TResult> this[string key]
        {
            get
            {
                Func<TArguments, TResult> temp;

                delegates.TryGetValue(key, out temp);

                return temp;
            }
            set
            {
                if (delegates.ContainsKey(key))
                {
                    Func<TArguments, TResult> temp;
                    delegates.TryGetValue(key, out temp);
                    delegates.Remove(key);

                    temp += value;

                    delegates.Add(key, temp);
                }
                else
                {
                    delegates.Add(key, value);
                }
            }
        }

        public TResult Throw(string key, TArguments args)
        {
            Func<TArguments, TResult> temp;

            delegates.TryGetValue(key, out temp);

            if (temp == null)
                return default(TResult);

            return temp(args);
        }

        public List<string> GetListOfCommands() => delegates.Keys.ToList();
    }
}

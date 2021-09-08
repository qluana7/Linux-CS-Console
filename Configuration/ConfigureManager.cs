using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using CsConsole.Command;

namespace CsConsole.Configuration
{
    public class CommandConfigureManager
    {
        public FileStream BaseStream { get; }

        private CommandConfigure _configure;
        public CommandConfigure Configure
        {
            get => JsonConvert.DeserializeObject<CommandConfigure>(File.ReadAllText(BaseStream.Name));//, _configure.Converters.ToArray());
            set
            {
                _configure = value;
                File.WriteAllText(BaseStream.Name, JsonConvert.SerializeObject(_configure));//, _configure.Converters.ToArray()));
            }
        }

        public CommandConfigureManager(string confile, CommandConfigure config = null)
            : this(new FileStream(confile, FileMode.OpenOrCreate), config) { }

        public CommandConfigureManager(FileStream constream, CommandConfigure config = null)
        {
            BaseStream = constream;
            Configure = config ?? new CommandConfigure()
            {
                Configure = new CommandExecutorConfigure()
            };
        }

        public void SetConfigure(string json)
            => File.WriteAllText(BaseStream.Name, json);
    }
}
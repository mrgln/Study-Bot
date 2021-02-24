using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.Net;
using Emzi0767.Utilities;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using DSharpPlus.CommandsNext.Attributes;
using Discord_Bot.Commands;
using DSharpPlus.Interactivity.Extensions;
using System.Linq;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;

namespace Discord_Bot.Commands
{
    public class CustomHelpFormatter : BaseHelpFormatter
    {
        protected DiscordEmbedBuilder _embed;

        public CustomHelpFormatter(CommandContext ctx) : base(ctx)
        {
           _embed = new DiscordEmbedBuilder();
        }

        public override BaseHelpFormatter WithCommand(Command command)
        {
            _embed.WithColor(DiscordColor.Aquamarine);
            _embed.AddField(command.Name, command.Description);

            return this;
        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> cmds)
        {
            string mathcommands = "`add` `min` `mply` `div` `sqrt` `sin` `cos` `tan` `cot` `raise` `avg`";
            string funcommands =  "`ping` `poll` `random`";
            string schedulecommands = "`8[letter]` `9[letter]` `sau` `sat` ";
            string url = "https://www.vippng.com/png/full/145-1451599_footer-footer-png.png";

            _embed.WithColor(DiscordColor.Aquamarine);
            _embed.WithImageUrl(url);
            _embed.WithDescription("Для информации о каждой команде напишите *nis help <название команды>*");

            _embed.WithTitle("Study Bot - simplifies the learning process");
            _embed.AddField("🖩 Math Commands", mathcommands);
            _embed.AddField("📋 Schedule Commands", schedulecommands);
            _embed.AddField("🎲 Fun Commands", funcommands);

            _embed.WithFooter("Type 'nis help <CommandName>' for details on command\nmade by mrgln ");
            return this;
        }

        public override CommandHelpMessage Build()
        {
            return new CommandHelpMessage(embed: _embed);
        }
    }
}

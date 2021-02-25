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

namespace Discord_Bot.Commands
{
    public class FunCommands: BaseCommandModule
    {

        [Command("ping")]
        public async Task Ping(CommandContext ctx)
        {
            Random random = new Random();
            int j = 1;
            if(j == random.Next(0,3))
                await ctx.Channel.SendMessageAsync("I've lost gg🏓").ConfigureAwait(false);
            else {await ctx.Channel.SendMessageAsync("pong 🏓").ConfigureAwait(false); }
        }


        [Command("responsemessage")]
        public async Task RespondMessage(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel && x.Author.Id == ctx.User.Id).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Content);
        }


        [Command("respondemoji")]
        public async Task RespondReaction(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel && x.User.Id == ctx.User.Id).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Emoji);
        }


        [Command("poll")]
        [Description("**Голосование/Опрос\n**" +
                    "= [duration] [Title for poll] [emoji] [emoji]...\n" +
                    "\n" +
                    "For example, *= 20s :pensive: :thumbsup: :champagne_glass:*\n" +
                    //"!There should be a space between emojis\nМежду смайликами должен быть пробел" +
                    "**!На данный момент команда работает некорректно!**\n")]
        public async Task Poll(CommandContext ctx, TimeSpan duration, params DiscordEmoji[] EmojiOptions)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var options = EmojiOptions.Select(x => x.ToString());

            await ctx.Channel.SendMessageAsync("Введите название для голосования").ConfigureAwait(false);

            var respond = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel && x.Author.Id == ctx.User.Id).ConfigureAwait(false);

            var PollEmbed = new DiscordEmbedBuilder
            {
                Title = respond.Result.Content,
            };

            var PollMessage = await ctx.Channel.SendMessageAsync(embed: PollEmbed).ConfigureAwait(false);

            foreach (var option in EmojiOptions)
            {
                await PollMessage.CreateReactionAsync(option).ConfigureAwait(false);
            }

            var result1 = await interactivity.CollectReactionsAsync(PollMessage, duration).ConfigureAwait(false);

            var results = result1.Distinct();

            var Results = results.Select(x => $"{x.Emoji}: {x.Total}");

            await ctx.Channel.SendMessageAsync(string.Join("\n", Results)).ConfigureAwait(false);
        }


        [Command ("random")]
        [Description( "Выбирает рандомное число из определенного диапозона\n"+
                      "*nis random [min] [max]*")]
        public async Task Random(CommandContext ctx, int x, int y)
        {
            var random = new Random();
            await ctx.RespondAsync($"`Your number is - {random.Next(x, y)}`");
        }
    }
}

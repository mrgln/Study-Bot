using DSharpPlus.CommandsNext;
using System;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity.Extensions;
using System.Linq;
using DSharpPlus.Interactivity.Enums;

namespace Discord_Bot.Commands
{
    public class FunCommands: BaseCommandModule
    {
        private DiscordEmoji[] _pollEmojiCache;

        [Command("ping")]
        public async Task Ping(CommandContext ctx)
        {
            Random random = new Random();
            int j = 1;
            if(j == random.Next(0,4))
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


        [Command("emojipoll"), Cooldown(2, 30, CooldownBucketType.Guild)]
        [Description("Голосование с результатами 'да' или 'нет'\n" +
                     "emojipoll [время, например 10s] [тема или вопрос]")]
        public async Task EmojiPollAsync(CommandContext commandContext, [Description("How long should the poll last. (e.g. 1m = 1 minute)")] TimeSpan duration, [Description("Poll question"), RemainingText] string question)
        {
            if (!string.IsNullOrEmpty(question))
            {
                var client = commandContext.Client;
                var interactivity = client.GetInteractivity();
                if (_pollEmojiCache == null)
                {
                    _pollEmojiCache = new[] {
                        DiscordEmoji.FromName(client, ":white_check_mark:"),
                        DiscordEmoji.FromName(client, ":x:")
                    };
                }

                // Creating the poll message
                var pollStartText = new StringBuilder();
                pollStartText.Append("**").Append("Голосование:").AppendLine("**");
                pollStartText.Append($"`{question}`");
                var pollStartMessage = await commandContext.RespondAsync(pollStartText.ToString());

                // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
                var pollResult = await interactivity.DoPollAsync(pollStartMessage, _pollEmojiCache, PollBehaviour.DeleteEmojis, duration);
                var yesVotes = pollResult[0].Total;
                var noVotes = pollResult[1].Total;

                // Printing out the result
                var pollResultText = new StringBuilder();
                pollResultText.AppendLine($"`{question}`");
                pollResultText.Append("Результат голосования: ");
                pollResultText.Append("**");
                if (yesVotes > noVotes)
                {
                    pollResultText.Append("Да");
                }
                else if (yesVotes == noVotes)
                {
                    pollResultText.Append("Не решено");
                }
                else if(noVotes ==0 && yesVotes >=0)
                {
                    pollResultText.Append("Да");
                }
                else if (yesVotes == 0 && noVotes >= 0)
                {
                    pollResultText.Append("Нет");
                }
                else
                {
                    pollResultText.Append("Нет");
                }
                pollResultText.Append("**");
                await commandContext.RespondAsync(pollResultText.ToString());
            }
            else
            {
                await commandContext.RespondAsync("Ошибка: вы не можете начать голосование без главного ворпоса или темы");
            }
        }


        [Command("guess")]
        [Description("Проверяет твои внутренние силы и способности предсказывания")]
        public async Task FightAsync(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            await ctx.RespondAsync("`У тебя есть пять попыток чтобы угадать число от 1 до 500, выиграешь - получишь роль 'пророк' и будешь находиться отдельно среди списка участников сервера`\nНапиши число:");

            var random = new Random();

            var chislo = random.Next(1, 499);

            var respond = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel && x.Author.Id == ctx.User.Id).ConfigureAwait(false);

            int answer = int.Parse(respond.Result.Content);

            if (answer == chislo)
            {
                await ctx.RespondAsync("`Ура ты обладаешь силами ванги, поздравляю!`:partying_face:");
                var role = ctx.Guild.GetRole(817829493929803776);
                await ctx.Member.GrantRoleAsync(role);
            }

            else 
            {
                if (answer < chislo)
                    await ctx.RespondAsync("`мало`");
                else if (answer >= 500)
                    await ctx.RespondAsync("`Число находится за пределами допустимых ответов`");
                else if (answer > chislo)
                    await ctx.RespondAsync("`много`");


                var respond2 = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel && x.Author.Id == ctx.User.Id).ConfigureAwait(false);

                int answer2 = int.Parse(respond2.Result.Content);

                if (answer2==chislo)
                {
                    await ctx.RespondAsync("`Ура ты обладаешь силами ванги, поздравляю!`:partying_face:");
                    var role = ctx.Guild.GetRole(817829493929803776);
                    await ctx.Member.GrantRoleAsync(role);
                }
                else
                {
                    if (answer2 < chislo)
                        await ctx.RespondAsync("`мало`");
                    else if (answer2 >= 500)
                        await ctx.RespondAsync("`Число находится за пределами допустимых ответов`");
                    else if(answer2>chislo)
                        await ctx.RespondAsync("`много`");
                  

                    var respond3 = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel && x.Author.Id == ctx.User.Id).ConfigureAwait(false);

                    int answer3 = int.Parse(respond3.Result.Content);

                    if (answer3==chislo)
                    {
                        await ctx.RespondAsync("`Ура ты обладаешь силами ванги, поздравляю!`:partying_face:");
                        var role = ctx.Guild.GetRole(817829493929803776);
                        await ctx.Member.GrantRoleAsync(role);
                    }
                    else
                    {
                        if (answer3 < chislo)
                            await ctx.RespondAsync("`мало`");
                        else if (answer3 >= 500)
                            await ctx.RespondAsync("`Число находится за пределами допустимых ответов`");
                        else if (answer3 > chislo)
                            await ctx.RespondAsync("`много`");

                        var respond4 = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel && x.Author.Id == ctx.User.Id).ConfigureAwait(false);

                        int answer4 = int.Parse(respond4.Result.Content);

                        if (answer4 == chislo)
                        {
                            await ctx.RespondAsync("`Ура ты обладаешь силами ванги, поздравляю!`:partying_face:");
                            var role = ctx.Guild.GetRole(817829493929803776);
                            await ctx.Member.GrantRoleAsync(role);
                        }
                        else
                        {
                            if (answer4 < chislo)
                                await ctx.RespondAsync("`мало`");
                            else if (answer4 >= 500)
                                await ctx.RespondAsync("`Число находится за пределами допустимых ответов`");
                            else if (answer4 > chislo)
                                await ctx.RespondAsync("`много`");


                            var respond5 = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel && x.Author.Id == ctx.User.Id).ConfigureAwait(false);

                            int answer5 = int.Parse(respond5.Result.Content);

                            if (answer5 == chislo)
                            {
                                await ctx.RespondAsync("`Ура ты обладаешь силами ванги, поздравляю!`:partying_face:");
                                var role = ctx.Guild.GetRole(817829493929803776);
                                await ctx.Member.GrantRoleAsync(role);
                            }
                            else
                            {
                                await ctx.RespondAsync($"`Число -> {chislo}`");
                                await ctx.RespondAsync("`Не расстраивайся, ты еще сможешь угадать`:wink:");
                            }
                        }
                    }
                }
            }
        }
    }
}

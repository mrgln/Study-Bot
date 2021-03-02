using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity.Extensions;
using System;

namespace Discord_Bot.Commands
{
    class SpecialCommands: BaseCommandModule
    {
        [Command("sau")]
        [Description("Shows schedule for Summative Assessment for the Unit")]
        public async Task SAU(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var message = await ctx.RespondAsync("`Choose grade. For example, 9`");
            var respond = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel && x.Author.Id ==ctx.User.Id).ConfigureAwait(false);
            
            if(respond.Result.Content == "9")
            { 
                var sauEmbed = new DiscordEmbedBuilder
                {
                Title = $"Расписание соров для {respond.Result.Content} класса",
                ImageUrl = "https://wmpics.pics/di-3QXL.png",
                };
                var sauMessage = await ctx.Channel.SendMessageAsync(embed: sauEmbed).ConfigureAwait(false);
            }
            else if(respond.Result.Content =="8")
            {
                var sauEmbed = new DiscordEmbedBuilder
                {
                    Title = $"Расписание соров для {respond.Result.Content} класса",
                    ImageUrl = "https://cdn1.savepice.ru/uploads/2021/2/24/9e888d852442e9004a2889b69f93dd92-full.png",
                };
                var sauMessage = await ctx.Channel.SendMessageAsync(embed: sauEmbed).ConfigureAwait(false);
            }
            else if (respond.Result.Content == "7")
            {
                var sauEmbed = new DiscordEmbedBuilder
                {
                    Title = $"Расписание соров для {respond.Result.Content} класса",
                    ImageUrl = "https://wmpics.pics/di-GXFY.png",
                };
                var sauMessage = await ctx.Channel.SendMessageAsync(embed: sauEmbed).ConfigureAwait(false);
            }
            else
            {
                var sauEmbed = new DiscordEmbedBuilder
                {
                    Description = "**Расписание соров остальных классов еще не загружено**",
                };
                var sauMessage = await ctx.Channel.SendMessageAsync(embed: sauEmbed).ConfigureAwait(false);
            }
        }


        [Command("sat")]
        [Description("Shows schedule for Summative Assessment for the Term")]
        public async Task SAT(CommandContext ctx)
        {
            var client = ctx.Client;
            var interactivity = client.GetInteractivity(); 

            var sauEmbed = new DiscordEmbedBuilder
            {
                Title = "Расписание СОЧ",
                ImageUrl = "https://wmpics.pics/di-TQVVL.jpg",
            };

            var pollMessage = await ctx.Channel.SendMessageAsync(embed: sauEmbed).ConfigureAwait(false);

            await pollMessage.CreateReactionAsync(DiscordEmoji.FromName(client, ":sob:")).ConfigureAwait(false);
            await Task.Delay(120);
            await pollMessage.CreateReactionAsync(DiscordEmoji.FromName(client, ":pensive:")).ConfigureAwait(false);
            await Task.Delay(120);
            await pollMessage.CreateReactionAsync(DiscordEmoji.FromName(client, ":neutral_face:")).ConfigureAwait(false);
            await Task.Delay(120);
            await pollMessage.CreateReactionAsync(DiscordEmoji.FromName(client, ":four_leaf_clover:")).ConfigureAwait(false);
        }

        [Command("9l")]
        [Description("Shows 9l's schedule for the week\n" +
                     "I.e. **nis 9l monday/tuesday/thursday/wednesday/thurdsay/friday**\n")]
        public async Task Schedule9l(CommandContext ctx, string _string)
        {
            string scheduleAccordingToDay;

            Random randomm = new Random();

            string[] meme = new string[15];
            meme[0] = "*хорошая работа олег, иди приляг отдохни*";
            meme[1] = "*:face_with_raised_eyebrow:*";
            meme[2] = "*бро, сегодня выходной, зачем тебе расписание...*";
            meme[3] = "*а вы тоже хотите узнать расписание уроков на выходных...*";
            meme[4] = "*отличная работа михаил*";
            meme[5] = "*вфполдажвылкщукшавдылаподкшоущаполавдыжлкопщшавызоклпдаолволафдопкшщпоалвдытапщкшуоашщпыокпдлавоыщкшпоалдыолдкопалвды ой простите мозг схватило*";
            meme[6] = "*я вам запрещать ходить в туалет, он сейчас на ремонте*";
            meme[7] = ":star2: *это звездочка удачи, сегодня тебе повезет*";
            meme[8] = "*ура выходной* :partying_face:";
            meme[9] = "*сейчас бы на стройку а не вот это все*:construction_site:";
            meme[11] = ":champagne_glass:";
            meme[12] = ":detective: *да да, у меня есть личные агенты а у тебя нет* :woman_detective:";
            meme[13] = "*с новым годом* :christmas_tree:";
            meme[14] = ":man_genie::-*привет я джин, загадывай что хочешь, только все равно ничего не получишь, я всего лишь дискорд эмоджи...*";

            switch (_string)
            {
                case "monday":scheduleAccordingToDay = "`08:30 - 08:50   География\n" + "09:00 - 09:20   География\n" + "09:30 - 09:50   Казахский язык и литература\n" +
                                                        "09:55 - 10:15   Казахский язык и литература\n" + "10:20 - 10:40   Химия\n" + "10:45 - 11:05   Химия\n" +
                                                        "11:20 - 11:40   Математика\n" + "11:45 - 12:05   Математика\n`";
                                                        await ctx.Channel.SendMessageAsync(scheduleAccordingToDay).ConfigureAwait(false);break;
                case "tuesday":scheduleAccordingToDay = "`08:30 - 08:50   Биология\n" + "09:00 - 09:20   Биология\n" + "09:30 - 09:50   Физика\n" +
                                                        "09:55 - 10:15   Физика\n" + "10:20 - 10:40   Английский язык\n" + "10:45 - 11:05   Английский язык\n" + "11:20 - 11:40   Русский язык\n" +
                                                        "11:45 - 12:05   Русский язык\n" + "13:15 - 13:35\n" + "13:45 - 14:05\n" + "14:15 - 14:35   Искусство\n" + "14:40 - 15:00   Искусство\n`";
                                                         await ctx.Channel.SendMessageAsync(scheduleAccordingToDay).ConfigureAwait(false);break;
                case "wednesday":scheduleAccordingToDay = "`08:30 - 08:50   История Казахстана\n" + "09:00 - 09:20   Кураторский час\n" + "09:30 - 09:50   Английский язык\n" +
                                                          "09:55 - 10:15   Основы права\n" + "10:20 - 10:40   Математика\n" + "10:45 - 11:05   Математика\n" +"11:20 - 11:40   Информатика\n" +
                                                          "11:45 - 12:05   Информатика\n" +"13:15 - 13:35   Физическая культура\n" +"13:45 - 14:05   Физическая культура\n`";
                                                          await ctx.Channel.SendMessageAsync(scheduleAccordingToDay).ConfigureAwait(false); break;
                case "thursday":scheduleAccordingToDay = "`08:30 - 08:50   Биология\n" +"09:00 - 09:20   Всемирная история\n" +"09:30 - 09:50   Физика\n" +"09:55 - 10:15   Физика\n" +"10:20 - 10:40   Химия\n" +
                                                          "10:45 - 11:05   Русский язык\n" +"11:20 - 11:40   Математика\n" +"11:45 - 12:05   Математика\n" +"13:15 - 13:35\n" +
                                                          "13:45 - 14:05\n" +"14:15 - 14:35\n"+"14:40 - 15:00   Самопознание\n`";
                                                          await ctx.Channel.SendMessageAsync(scheduleAccordingToDay).ConfigureAwait(false); break;
                case "friday": scheduleAccordingToDay = "`08:30 - 08:50\n" +"09:00 - 09:20\n" +"09:30 - 09:50   Английский язык\n" +"09:55 - 10:15   Английский язык\n" +"10:20 - 10:40   Русская литература\n" +
                                                          "10:45 - 11:05   Русская литература\n" +"11:20 - 11:40   Казахский язык и литература\n" +"11:45 - 12:05   Казахский язык и литература\n`";
                                                          await ctx.Channel.SendMessageAsync(scheduleAccordingToDay).ConfigureAwait(false); break;
                case "saturday": scheduleAccordingToDay = meme[randomm.Next(0, meme.Length)]; await ctx.Channel.SendMessageAsync(scheduleAccordingToDay).ConfigureAwait(false); break;
                case "sunday": scheduleAccordingToDay = meme[randomm.Next(0, meme.Length)]; await ctx.Channel.SendMessageAsync(scheduleAccordingToDay).ConfigureAwait(false); break;
                default: scheduleAccordingToDay = "";
                    await ctx.Channel.SendMessageAsync(scheduleAccordingToDay).ConfigureAwait(false);
                    break;
            }
        }
        [Command("9l")]
        public async Task Schedule9l(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Пожалуйста введите день недели на английском. Например *nis 9l monday*").ConfigureAwait(false);
        }
    }
}

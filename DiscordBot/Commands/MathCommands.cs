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

namespace Discord_Bot.Commands
{
    public class MathCommands : BaseCommandModule
    {
        [Command("add")]
        [Description("Adds numbers together\n*nis add [number1] [number2]...*")]
        public async Task Addition(CommandContext ctx, params System.Single[] Numbers)
        {
            System.Single result = 0f;
            for (int i = 0; i < Numbers.Length; i++)
            {
                result += Numbers[i];
            }
            await ctx.Channel
                .SendMessageAsync(($"`{result}`").ToString())
                .ConfigureAwait(false);
        }


        [Command("mply")]
        [Description("Multiplies numbers\n*nis mply [number1] [number2]...*")]
        public async Task Multiplication(CommandContext ctx, params System.Single[] Numbers)
        {
            System.Single result = 1f;
            for (int i = 0; i < Numbers.Length; i++)
            {
                result *= Numbers[i];
            }
            await ctx.Channel
                .SendMessageAsync(($"`{result}`").ToString())
                .ConfigureAwait(false);
        }


        [Command("div")]
        [Description("Divides all numbers\n*nis div [number1] [number2]...*")]
        public async Task Division(CommandContext ctx, params System.Single[] Numbers)
        {
            System.Single result = Numbers[0];

            for (int i = 1; i < Numbers.Length; i++)
            {

                result /= Numbers[i];
            }
            await ctx.Channel
                .SendMessageAsync(($"`{result}`").ToString())
                .ConfigureAwait(false);
        }


        [Command("min")]
        [Description("Subtracts all numbers\n*nis min [number1] [number2]...*")]
        public async Task Subtraction(CommandContext ctx, params System.Single[] Numbers)
        {
            System.Single result = Numbers[0];
            for (int i = 1; i < Numbers.Length; i++)
            {
                result -= Numbers[i];
            }
            await ctx.Channel
                .SendMessageAsync(($"`{result}`").ToString())
                .ConfigureAwait(false);
        }


        [Command("sin")]
        [Description("Sinus(degrees) = ?\n*nis sin [degrees]*")]
        public async Task Sinus(CommandContext ctx, double x)
        {
            double radian;
            radian = x * Math.PI / 180;
            await ctx.Channel
                .SendMessageAsync(($"`Синус({x}°) = {Math.Round(Math.Sin(radian), 2)}`").ToString())
                .ConfigureAwait(false);
        }


        [Command("cos")]
        [Description("Cosine(degrees) = ?\n*nis cos [degrees]*")]
        public async Task Cosine(CommandContext ctx, double x)
        {
            double radian;
            radian = x * Math.PI / 180;
            await ctx.Channel
                .SendMessageAsync(($"`Косинус({x}°) = {Math.Round(Math.Cos(radian), 2)}`").ToString())
                .ConfigureAwait(false);
        }


        [Command("tan")]
        [Description("Tangent(degrees) = ?\n*nis tan [degrees]*")]
        public async Task Tangent(CommandContext ctx, double x)
        {
            double radian;
            radian = x * Math.PI / 180;
            await ctx.Channel
                .SendMessageAsync(($"`Тангенс({x}°) = {Math.Round(Math.Tan(radian), 2)}`").ToString())
                .ConfigureAwait(false);
        }


        [Command("cot")]
        [Description("Cotangent(degrees) = ?\n*nis cot [degrees]*")]
        public async Task Cotangent(CommandContext ctx, double x)
        {
            double radian;
            radian = x * Math.PI / 180;
            await ctx.Channel
                .SendMessageAsync(($"`Котангенс({x}°) = {Math.Round(1 / Math.Tan(radian), 2)}`").ToString())
                .ConfigureAwait(false);
        }


        [Command("raise")]
        [Description("Raises number to the power of X\n*nis raise [number] [X]* ")]
        public async Task Raise(CommandContext ctx, double x, double y)
        {
            await ctx.Channel
               .SendMessageAsync(($"`{x}^{y} = { Math.Pow(x, y)}`").ToString())
               .ConfigureAwait(false);
        }


        [Command("sqrt")]
        [Description("Finds square root\n*nis sqrt [number]*")]
        public async Task Sqrt(CommandContext ctx, double x)
        {
            await ctx.Channel
                .SendMessageAsync(($"`√{x} = {Math.Sqrt(x)}`").ToString())
                .ConfigureAwait(false);
        }


        [Command("avg")]
        [Description("Finds average of all numbers\n*nis avg [number1] [number2]...")]
        public async Task Average(CommandContext ctx, params System.Single[] Numbers)
        {
            System.Single result = Numbers[0];
            for (int i = 1; i < Numbers.Length; i++)
            {
                result += Numbers[i];
            }
            await ctx.Channel
                .SendMessageAsync(($"`Average number is {result/Numbers.Length}`").ToString())
                .ConfigureAwait(false);
        }
        //should add acos asin atan acot
    }
}

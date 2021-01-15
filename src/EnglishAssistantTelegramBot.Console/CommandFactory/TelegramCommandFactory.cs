﻿using System;
using System.Collections.Generic;
using System.Text;
using EnglishAssistantTelegramBot.Console.Client;
using EnglishAssistantTelegramBot.Console.Commands.Abstract;
using EnglishAssistantTelegramBot.Console.Commands.Concrete;
using EnglishAssistantTelegramBot.Console.Repository.Abstract;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EnglishAssistantTelegramBot.Console.CommandFactory
{
    public class TelegramCommandFactory : ITelegramCommandFactory
    {
        private ITelegramBotClient TelegramBotClient { get; }
        private readonly IWordRepository _wordRepository;
        private readonly IStoryRepository _storyRepository;
        private readonly IQuoteRepository _quoteRepository;
        private readonly IVolunteerPageRepository _volunteerPageRepository;

        public TelegramCommandFactory(ITelegramClient telegramClient, IWordRepository wordRepository, IStoryRepository storyRepository, IQuoteRepository quoteRepository, IVolunteerPageRepository volunteerPageRepository)
        {
            _wordRepository = wordRepository;
            _storyRepository = storyRepository;
            _quoteRepository = quoteRepository;
            _volunteerPageRepository = volunteerPageRepository;
            TelegramBotClient = telegramClient.GetInstance();
        }

        public ICommand CreateCommand(Message command)
        {
            switch (command.Text)
            {
                case "/start":
                    return new ShowCommand(TelegramBotClient);
                case "/sendnewstory":
                    return new SendStoryCommand(TelegramBotClient, _storyRepository);
                case "/sendnewword":
                    return new SendNewWordCommand(TelegramBotClient, _wordRepository);
                case "/sendnewquote":
                    return new SendNewQuoteCommand(TelegramBotClient, _quoteRepository);
                case "/supportvolunteerpages":
                    return new SendVolunteerPageCommand(TelegramBotClient, _volunteerPageRepository);
                default:
                    return new ShowCommand(TelegramBotClient);
            }
        }
    }
}

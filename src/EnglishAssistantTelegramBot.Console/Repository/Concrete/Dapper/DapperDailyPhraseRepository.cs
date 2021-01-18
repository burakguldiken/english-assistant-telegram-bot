﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EnglishAssistantTelegramBot.Console.Configuration.Context;
using EnglishAssistantTelegramBot.Console.Entities;
using EnglishAssistantTelegramBot.Console.Repository.Abstract;
using EnglishAssistantTelegramBot.Console.Repository.Concrete.Dapper.BaseRepository;
using MySql.Data.MySqlClient;

namespace EnglishAssistantTelegramBot.Console.Repository.Concrete.Dapper
{
    public class DapperDailyPhraseRepository : DapperBaseRepository<DailyPhrase>, IDailyPhraseRepository
    {
        private readonly string _connectionString;

        public DapperDailyPhraseRepository(IConfigurationContext configurationContext) : base(configurationContext)
        {
            _connectionString = configurationContext.MySQLConnectionString;
        }

        public async Task<DailyPhrase> GetAnyDailyPhraseAsync()
        {
            const string sql = "SELECT * FROM dailyphrase ORDER BY RAND() LIMIT 1;";

            await using var connection = new MySqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<DailyPhrase>(new CommandDefinition(sql));
        }
    }
}

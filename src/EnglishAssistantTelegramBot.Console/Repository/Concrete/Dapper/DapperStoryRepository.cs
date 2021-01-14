﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EnglishAssistantTelegramBot.Console.Entities;
using EnglishAssistantTelegramBot.Console.Repository.Abstract;
using EnglishAssistantTelegramBot.Console.Repository.Concrete.Dapper.BaseRepository;
using MySql.Data.MySqlClient;

namespace EnglishAssistantTelegramBot.Console.Repository.Concrete.Dapper
{
    public class DapperStoryRepository : DapperBaseRepository<Story>, IStoryRepository
    {
        // I will move base from here to app settings or in another file.
        private string connectionString = "Server=localhost; Database=englishreaderassistanttelegrambot; Uid=root; Pwd=12345;";


        public async Task<Story> GetAnyStoryAsync()
        {
            const string sql = "SELECT * FROM story ORDER BY RAND() LIMIT 1;";

            await using var connection = new MySqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<Story>(new CommandDefinition(sql));
        }
    }
}

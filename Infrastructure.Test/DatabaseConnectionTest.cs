using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Xunit;

namespace Infrastructure.Test
{
    public class DatabaseConnectionTest
    {
        private readonly string _connectionString;

        public DatabaseConnectionTest()
        {
            _connectionString = "Server=localhost;Database=TS;Integrated Security=true;TrustServerCertificate=True;Encrypt=True";
        }

        [Fact]
        public void ConnectDbWithConnectionString_ReturnNoError()
        {
            // Act
            using SqlConnection db = new(_connectionString);
            db.Open();
        }

        [Fact]
        public void ConnectDbWithConnectionString_ReturnTodayDate()
        {
            // Act
            using SqlConnection db = new(_connectionString);
            DateTime actual = db.Query<DateTime>("SELECT GETDATE()").FirstOrDefault();
            // Assert
            actual.Date.Should().Be(DateTime.Now.Date);
        }
    }
}
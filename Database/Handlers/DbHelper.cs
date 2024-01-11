using Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database.Models;

namespace Database.Handlers
{
    public static class DbHelper
    {

        public static async Task<IReadOnlyList<User>> ListAllUserAsync(MusicContext context)
        {
            var results = context.Users.ToListAsync();
            return await results;
        }

    }
}

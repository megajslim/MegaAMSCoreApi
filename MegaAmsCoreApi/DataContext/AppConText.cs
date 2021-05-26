using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MegaAmsCoreApi.DataContext
{
    public class AppConText : DbContext
    {
        public AppConText()
        {

        }

        public AppConText(DbContextOptions<AppConText> options) : base(options) { }
    }
}

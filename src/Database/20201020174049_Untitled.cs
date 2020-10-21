using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;
using System.Data;

namespace Locadora.Database
{
    [Migration(20201020174049)]
    public class Migration20201020174049 : FluentMigration
    {
        public override void Up(SchemaAction schema)
        {

            schema.RenameTable("books", "t_books");
   
        }

        public override void Down(SchemaAction schema)
        {
            schema.RenameTable("books", "t_books");
        }
    }

}
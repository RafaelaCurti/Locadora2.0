using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;
using System.Data;

namespace Locadora.Database
{
    [Migration(20201020173121)]
    public class Migration20201020173121 : FluentMigration
    {
        public override void Up(SchemaAction schema)
        {
            //schema.AddTable("books", t =>
            //{
            //    t.AddString("title");
            //    t.AddInt32("pages");
            //    t.AddString("author");
            //    t.AddInt32("edition");
            //    t.AddInt32("year");
            //});
        }

        public override void Down(SchemaAction schema)
        {
            schema.RemoveTable("books");
        }
    }

}
﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MyFirstEFCoreApp.Entity;
using System;

namespace MyFirstEFCoreApp
{
    internal static class Commands
    {
        public static void ListAll()
        {
            using (var db = new EFCoreDBContext()) //#A
            {
                foreach (var book in db.Books.AsNoTracking() //#B
                    .Include(book => book.Author)) //#C
                {
                    var webUrl = book.Author.WebUrl ?? "- no web url given -";
                    Console.WriteLine($"{book.Title} by {book.Author.Name}");
                    Console.WriteLine("     Published on " +
                                      $"{book.PublishedOn:dd-MMM-yyyy}. {webUrl}");
                }
            }
        }

        public static void ChangeWebUrl()
        {
            Console.WriteLine("New Quantum Networking WebUrl > ");
            var newWebUrl = Console.ReadLine();

            using (var db = new EFCoreDBContext())
            {
                var singleBook = db.Books
                    .Include(book => book.Author)
                    .Single(book => book.Title == "Quantum Networking");
                singleBook.Author.WebUrl = newWebUrl;
                db.SaveChanges();
                Console.WriteLine("... SabedChanges called.");
            }
        }

        public static bool WipeCreateSeed(bool onlyIfNoDatabase)
        {
            using (var db = new EFCoreDBContext())
            {
                if (onlyIfNoDatabase && (db.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                    return false;

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                if (!db.Books.Any())
                {
                    WriteTestData(db);
                    Console.WriteLine("Seeded database");
                }
            }

            return true;
        }

        public static void WriteTestData(this EFCoreDBContext db)
        {
            var martinFowler = new Author
            {
                Name = "Martin Fowler",
                WebUrl = "http://martinfowler.com/"
            };

            var books = new List<Book>
        {
            new Book
            {
                Title = "Refactoring",
                Description = "Improving the design of existing code",
                PublishedOn = new DateTime(1999, 7, 8),
                Author = martinFowler
            },
            new Book
            {
                Title = "Patterns of Enterprise Application Architecture",
                Description = "Written in direct response to the stiff challenges",
                PublishedOn = new DateTime(2002, 11, 15),
                Author = martinFowler
            },
            new Book
            {
                Title = "Domain-Driven Design",
                Description = "Linking business needs to software design",
                PublishedOn = new DateTime(2003, 8, 30),
                Author = new Author { Name = "Eric Evans", WebUrl = "http://domainlanguage.com/" }
            },
            new Book
            {
                Title = "Quantum Networking",
                Description = "Entangled quantum networking provides faster-than-light data communications",
                PublishedOn = new DateTime(2057, 1, 1),
                Author = new Author { Name = "Future Person", WebUrl = "" }
            }
        };

            db.Books.AddRange(books);
            db.SaveChanges();
        }
    }
}

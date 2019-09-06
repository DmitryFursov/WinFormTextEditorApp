using DataBaseHandler.DbContexts;
using DataBaseHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBaseHandler
{
    public class TextFileDbHandler : ITextFileDbHandler
    {
        private static TextFileDbHandler instance;

        private static object syncRoot = new object();

        protected TextFileDbHandler()
        {
        }

        public static TextFileDbHandler Instance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new TextFileDbHandler();
                }
            }

            return instance;
        }

        /// <summary>
        /// Reads all the file titles from Db
        /// </summary>
        /// <returns></returns>
        public List<string> ReadFileTitles()
        {
            using(var context = new FileContext())
            {
                 return context.Files.Select(x => x.Title).OrderBy(x => x).ToList();
            }
        }

        public void AddFile(FileModel file)
        {
            using (var context = new FileContext())
            {
                context.Files.Add(file);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the file if new Title is free, otherwise, throw the exception
        /// </summary>
        /// <param name="oldfile">The Old File with old title and Content</param>
        /// <param name="newFile">The new file with new title and Content</param>
        /// <exception cref="InvalidOperationException">Occurs when do the new file title is not free.</exception>
        public void UpdateFile(FileModel oldfile, FileModel newFile)
        {
            using (var context = new FileContext())
            {
                if (IsTitleExists(newFile.Title))
                {
                    throw new InvalidOperationException($"The file with title \"{newFile.Title}\" already exists");
                }

                if (IsTitleExists(oldfile.Title)) // needs to be locked or something
                {
                    context.Files.Remove(oldfile);
                }
                
                context.Files.Add(newFile);
                context.SaveChanges();
            }
        }

        public void DeleteFile(FileModel file)
        {
            using (var context = new FileContext())
            {
                context.Files.Remove(file);
                context.SaveChanges();
            }
        }

        private bool IsTitleExists(string fileTitle)
        {
            using (var context = new FileContext())
            {
                return context.Files.Any(file => file.Title == fileTitle);
            }
        }

        public FileModel GetFile(string fileTitle)
        {

            using (var context = new FileContext())
            {
                return context.Files.Find(fileTitle);
            }
        }
    }
}

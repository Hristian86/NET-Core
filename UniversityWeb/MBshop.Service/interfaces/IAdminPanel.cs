﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MBshop.Models;

namespace MBshop.Service.interfaces
{
    public interface IAdminPanel
    {
        Task<Movies> FindMovieById(int? id);

        Task<string> CreateMovie(Movies movie);

        List<Movies> GetMovies();

        Task<string> UpdateMovie(Movies movie);

        Task<string> RemoveMovie(int movieId);

        bool MovieExist(int id);

        Task<Books> FindBookById(int? id);

        Task<string> CreateBook(Books book);

        List<Books> GetBooks();

        Task<string> UpdateBook(Books book);

        Task<string> RemoveBook(int bookId);

        bool BookExist(int id);

    }
}
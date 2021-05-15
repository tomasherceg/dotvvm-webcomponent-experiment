﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWebComponents.ViewModels.FluentUI
{
    public class DataGridViewModel : DotvvmViewModelBase
    {
        private static IQueryable<Customer> FakeDb()
        {
            return new[]
            {
                new Customer(0, "Dani Michele"), new Customer(1, "Elissa Malone"), new Customer(2, "Raine Damian"),
                new Customer(3, "Gerrard Petra"), new Customer(4, "Clement Ernie"), new Customer(5, "Rod Fred"),
                new Customer(6, "Oliver Carr"), new Customer(7, "Jackson James"), new Customer(8, "Dexter Nicholson"),
                new Customer(9, "Jamie Rees"), new Customer(10, "Jackson Ross"), new Customer(11, "Alonso Sims"),
                new Customer(12, "Zander Britt"), new Customer(13, "Isaias Ford"), new Customer(14, "Braden Huffman"),
                new Customer(15, "Frederick Simpson"), new Customer(16, "Charlie Andrews"), new Customer(17, "Reuben Byrne")
            }.AsQueryable();
        }

        public GridViewDataSet<Customer> Customers { get; set; } = new GridViewDataSet<Customer>() { PagingOptions = { PageSize = 4 } };

        public override Task PreRender()
        {
            if (Customers.IsRefreshRequired)
            {
                var queryable = FakeDb();
                Customers.LoadFromQueryable(queryable);
            }
            return base.PreRender();
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Customer()
        {
            // NOTE: This default constructor is required. 
            // Remember that the viewmodel is JSON-serialized
            // which requires all objects to have a public 
            // parameterless constructor
        }

        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}


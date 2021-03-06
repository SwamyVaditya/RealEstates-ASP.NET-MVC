﻿namespace RealEstates.Web.Services
{
    using Data.Common.Repositories;
    using Data.Models;
    using Models.Properties;
    using RealEstates.Web.Services.Contracts;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper.QueryableExtensions;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Infrastructure.Mapping;
    public class PropertyService : Controller, IPropertyService
    {
        private IDeletableEntityRepository<Property> modifiableProperties;
        private IRepository<Property> realDeleteProperties;
        private IDeletableEntityRepository<User> users;

        public PropertyService(IDeletableEntityRepository<Property> modifiableProperties,
            IDeletableEntityRepository<User> users,
            IRepository<Property> realDeleteProperties)
        {
            this.modifiableProperties = modifiableProperties;
            this.users = users;
            this.realDeleteProperties = realDeleteProperties;
        }

        public PropertyDetailsViewModel PropertyDetails(int id)
        {
            var property = modifiableProperties
               .All()
               .Where(pr => pr.Id == id)
               .To<PropertyDetailsViewModel>()
               .FirstOrDefault();

            return property;
        }

        public Property CreateProperty(AddPropertyViewModel model, string currentUser)
        {
            var property = new Property()
            {
                Id = model.Id,
                AuthorId = currentUser,
                Title = model.Title,
                Description = model.Description,
                Sity = model.Sity,
                Price = model.Price,
                PropertyStatus = model.PropertyStatus,
                PropertyType = model.PropertyType
            };

            return property;
        }

        public Property CreateProperty(Property model, string currentUser)
        {
            var property = new Property()
            {
                Id = model.Id,
                AuthorId = currentUser,
                Title = model.Title,
                Description = model.Description,
                Sity = model.Sity,
                Price = model.Price,
                PropertyStatus = model.PropertyStatus,
                PropertyType = model.PropertyType
            };

            return property;
        }
        
        public IList<MyAdsViewModel> GetMyAds(string currentUser)
        {
            var myAds = this.realDeleteProperties
                .All()
                .OrderByDescending(x => x.DeletedOn)
                .Where(x => x.AuthorId == currentUser)
                .To<MyAdsViewModel>()
                .ToList();

            return myAds;
        }
    }
}
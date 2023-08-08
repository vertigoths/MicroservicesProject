﻿using FreeCourse.Services.Catalog.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FreeCourse.Services.Catalog.Dtos.DefaultDtos
{
    public class CourseDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public string Picture { get; set; }

        public DateTime CreationDate { get; set; }

        public FeatureDto Feature { get; set; }

        public string CategoryId { get; set; }

        public CategoryDto Category { get; set; }
    }
}

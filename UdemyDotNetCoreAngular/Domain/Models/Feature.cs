﻿namespace UdemyDotNetCoreAngular.Domain.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelId { get; set; }

        public virtual Model Model { get; set; }
    }
}
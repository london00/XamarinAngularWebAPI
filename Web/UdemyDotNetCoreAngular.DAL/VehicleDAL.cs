﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyDotNetCoreAngular.Domain;
using UdemyDotNetCoreAngular.Domain.Models;
using UdemyDotNetCoreAngular.DTO.Filters;

namespace UdemyDotNetCoreAngular.DAL
{
    public class VehicleDAL : IVehicleDAL
    {
        private readonly VegaDBContext db;

        public VehicleDAL(VegaDBContext db)
        {
            this.db = db;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            vehicle.LastUpdate = DateTime.Now;
            db.Vehicles.Add(vehicle);
        }

        public async Task<Vehicle> GetVehicleById(int id)
        {
            return await db.Vehicles
                .Include(x => x.VehicleFeatures)
                    .ThenInclude(x => x.Feature)
                .Include(x => x.Model)
                    .ThenInclude(x => x.Make)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            vehicle.LastUpdate = DateTime.Now;
            db.Update(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            db.Remove(vehicle);
        }

        public async Task<List<Vehicle>> GetVehicles(VehicleFilterDTO filter)
        {
            var query = db.Vehicles
               .Include(x => x.VehicleFeatures)
                   .ThenInclude(x => x.Feature)
               .Include(x => x.Model)
                   .ThenInclude(x => x.Make)
                .AsQueryable();

            if (filter.MakeId.HasValue)
            {
                query = query.Where(x => x.Model.MakeId == filter.MakeId);
            }

            if (filter.ModelId.HasValue)
            {
                query = query.Where(x => x.Model.Id == filter.ModelId);
            }

            return await query.ToListAsync();
        }
    }
}

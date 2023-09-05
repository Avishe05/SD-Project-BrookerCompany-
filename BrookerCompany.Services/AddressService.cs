using BrookerCompany.Data;
using BrookerCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrookerCompany.Services
{
    public class AddressService
    {
        private AppDbContext context;

        public string AddAddress(string addressName, string townName)
        {
            StringBuilder message = new StringBuilder();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(addressName))
            {
                message.AppendLine($"Invalid {nameof(addressName)}");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(townName))
            {
                message.AppendLine($"Invalid {nameof(townName)}");
                isValid = false;
            }

            using (var context = new AppDbContext())
            {
                var town = context.Towns.FirstOrDefault(t => t.Name == townName);
                if (town == null)
                {
                    town = new Town() { Name = townName };
                    context.Towns.Add(town);
                    context.SaveChanges(); // Save the town first before using it in the address
                }

                var address = context.Addresses.FirstOrDefault(a => a.Name == addressName && a.Town.Name == townName);
                if (address == null)
                {
                    address = new Address() { Name = addressName, Town = town };
                    context.Addresses.Add(address);
                    context.SaveChanges();
                    message.AppendLine($"Address {addressName} in {townName} is added!");
                }
                else
                {
                    message.AppendLine($"Address {addressName} in {townName} already exists.");
                }

                return message.ToString().TrimEnd();
            }
        }
        public string GetAddresssName(int id)
        {
            using (context = new AppDbContext())
            {
                Brooker b = context.Brookers.Find(id);
                return b.Address.Name.ToString();

            }
        }


        public Address GetAddressById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Invalid address id");
            }
            using (context = new AppDbContext())
            {
                Address address = this.context.Addresses.FirstOrDefault(x => x.Id == (id));
                return address;
            }
        }
        public List<string> GetAddressNames()
        {
            using (context = new AppDbContext())
            {
                List<string> addresses = context.Addresses.Select(x => x.Name).ToList();
                return addresses;
            }
        }
    }
}

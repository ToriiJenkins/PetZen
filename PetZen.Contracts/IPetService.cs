using PetZen.Models;
using PetZen.Models.PetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Contracts
{
    //An interface is a type that defines a contract between an object and the interface
    public interface IPetService
    {
        //properties
        //method signatures
        

        bool CreatePet(PetCreate petToCreate);

        IEnumerable<PetListItem> GetPets();

        PetDetail GetPetById(int petId);
        bool UpdatePet(PetEdit petToEdit);

        bool DeletePet(int PetId);

    }
}

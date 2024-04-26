using BarangayInformationManagement.Models;
using BarangayInformationManagement.Models.Domains;
using BarangayInformationManagement.Models.Dtos;
using BarangayInformationManagement.Repositories.Interface;
using BarangayInformationManagement.Services.Interface;

namespace BarangayInformationManagement.Services
{
    public class BarangayOfficialService : IBarangayOfficialService
    {
        private readonly IBarangayOfficialRepository barangayOfficialRepository;

        public BarangayOfficialService(IBarangayOfficialRepository barangayOfficialRepository)
        {
            this.barangayOfficialRepository = barangayOfficialRepository;
        }

        public async Task<BarangayOfficialInfoModel> Get(string id)
        {
             var existingBarangayOfficial = await barangayOfficialRepository.Get(id);
             return new BarangayOfficialInfoModel
             {
                 id = existingBarangayOfficial.id,
                 fullname = existingBarangayOfficial.fullname,
                 chairmanship = existingBarangayOfficial.chairmanship,
                 position = existingBarangayOfficial.position,
                 status = existingBarangayOfficial.status,
                 term_start = existingBarangayOfficial.term_start,
                 term_end = existingBarangayOfficial.term_end,
             };
        }

        public async Task<IEnumerable<BarangayOfficialInfoModel>> GetAll()
        {
             var officials = new List<BarangayOfficialInfoModel>();

             var domainOfficials = await barangayOfficialRepository.GetAll();

             foreach(var item in domainOfficials)
             {
                 officials.Add(new BarangayOfficialInfoModel
                 {
                     id = item.id,
                     fullname = item.fullname,
                     chairmanship = item.chairmanship,
                     position = item.position,
                     status = item.status,
                     term_start = item.term_start,
                     term_end = item.term_end,
                 });
             }

             return officials;
        }

        public async Task<ResponseModel> Insert(BarangayOfficialInfoModel model)
        {
            try
            {
                var domainOfficial = new BarangayOfficialModel
                {
                    fullname = model.fullname,
                    chairmanship = model.chairmanship,
                    position = model.position,
                    status = model.status,
                    term_start=model.term_start,
                    term_end=model.term_end,
                };

                await barangayOfficialRepository.Insert(domainOfficial);

                return new ResponseModel
                {
                    isSuccess = true,
                    successMessage = "Successfully added the Barangay Official."
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseModel
                {
                    isError = true,
                    errorMessage = "Internal Server Error"
                };
            }
        }

        public async Task<ResponseModel> Delete(string id)
        {
            try
            {
                await barangayOfficialRepository.Delete(id);
                return new ResponseModel
                {
                    isSuccess = true,
                    successMessage = "Barangay Official has been permanently deleted."
                };
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return new ResponseModel
                {
                    isError = true,
                    errorMessage = "Internal Server Error"
                };
            }
        }


        public async Task<ResponseModel> Update(BarangayOfficialInfoModel model)
        {
            try
            {
                var existingBarangayOfficial = await barangayOfficialRepository.Get(model.id);
                if (existingBarangayOfficial == null)
                    return new ResponseModel
                    {
                        isWarning = true,
                        warningMessage = "Barangay Official does not exist."
                    };

                existingBarangayOfficial.fullname = model.fullname;
                existingBarangayOfficial.position = model.position;
                existingBarangayOfficial.status = model.status;
                existingBarangayOfficial.chairmanship = model.chairmanship;
                existingBarangayOfficial.term_start = model.term_start;
                existingBarangayOfficial.term_end = model.term_end;

                await barangayOfficialRepository.Update(model.id, existingBarangayOfficial);

                return new ResponseModel
                {
                    isSuccess = true,
                    successMessage = "Successfully updated the Barangay Official"
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseModel
                {
                    isError = true,
                    errorMessage = "Internal Server Error"
                };
            }
        }
    }
}

using BackEndTest.Domain.Entities;
using BackEndTest.Domain.Entities.Externals;
using BackEndTest.Infrastructure.Context;
using BackEndTest.Infrastructure.Interfaces;
using Lucene.Net.Support;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AppSettings = BackEndTest.Domain.Entities.AppSettings;

namespace BackEndTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppSettings _appSetting;
        private readonly IRepository<Customer, CustomerDBContext> _CustomerRepo;
        private readonly IHttpClientFactory _httpClientFactory;
               

        public CustomerController(CustomerDBContext context, IRepository<Customer, CustomerDBContext> CustomerRepo, IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSettings)
        {

            _CustomerRepo = CustomerRepo;
            _httpClientFactory = httpClientFactory;
            _appSetting = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            List<Customer> customers = _CustomerRepo.GetAllEntity().ToList();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _CustomerRepo.GetEntityByID(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([Bind("Id,Firstname,Lastname,PhoneNumber,LGA, StateofResidence ")] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            if (customer != null)
            {
                //validate lga against the selected state here
                //if valid proceed else return
                var isMapped = StateandLGAApi.IsMapped(customer.StateofResidence, customer.LGA);
                if(!isMapped)
                    return BadRequest("LGA and the selected State must map!");
                TokenSimulation tok = new TokenSimulation();
             
                SmsMessage smsMessage = new SmsMessage();
                smsMessage.Token = tok.GenerateRefreshToken();
                //send sms to user to complete onboarding process
                SendSms(smsMessage);
                _CustomerRepo.InsertEntity(customer);

                await _CustomerRepo.Save();
                return Ok(customer);
            }
            return BadRequest("Wrong data");
        }

        [HttpGet]
        public IActionResult SendSms(SmsMessage model)
        {            
            //Send built sms message to user
            return Ok("Success");
        }



        [HttpGet]
        public async Task<IActionResult> GetBanks()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _appSetting.BankUrl);
            var subKey = _appSetting.SubKey;
            request.Headers.Add("subscriptionkey", subKey);
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            var banks = await response.Content.ReadAsStringAsync();
            
            return Ok(JsonConvert.DeserializeObject<Banks>(banks));
        }

        [HttpGet, Route("api/LoadJsonFileData")]
        public IActionResult LoadJsonFileData()
        {
            List<StateandLGAs> items = new List<StateandLGAs>();
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "StateandLGA.json");
            
            using (StreamReader r = new StreamReader(fullPath))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<StateandLGAs>>(json);
            }

            return Ok(items);
        }

       
    }
}
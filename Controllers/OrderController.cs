﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderItem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OrderItem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        List<Cart> cart;
        [HttpPost]
        public async Task<List<Cart>> CreateCart(int menuitemid)
        {
            var client = new HttpClient();
            string url = $"https://localhost:44357/api/MenuItem/id?id={menuitemid}";
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Clear();
            //Define request data format  
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var Res = client.GetAsync(url).Result;
            //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
            HttpResponseMessage Res = await client.GetAsync(url);

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var ProductResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list  
                var PrdInfo = JsonConvert.DeserializeObject<MenuItem>(ProductResponse);
                cart = new List<Cart>()
            {
                new Cart()
                {
                    Id=1,
                    userId=1,
                    menuitemId=PrdInfo.Id,
                    menuitemName=PrdInfo.Name
                }
            };
                return cart;
            }
            else
            {
                return null;
            }
            
            
        }
    }
}

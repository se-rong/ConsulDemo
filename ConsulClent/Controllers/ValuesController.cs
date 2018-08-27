using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ConsulClent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            using (var consul = new Consul.ConsulClient(c =>
            {
                c.Address = new Uri("http://127.0.0.1:8500"); //Consul地址
            }))
            {
                //取出全部的ConsulDemo服务
                var services = consul.Agent.Services().Result.Response.Values.Where(p => p.Service.Equals("ConsulDemo", StringComparison.OrdinalIgnoreCase));

                //客户端负载均衡，随机选出一台服务
                Random rand = new Random();
                var index = rand.Next(services.Count());
                var s = services.ElementAt(index);
                Console.WriteLine($"Index={index},ID={s.ID},Service={s.Service},Addr={s.Address},Port={s.Port}");

                //向服务发送请求
                using (var httpClient = new HttpClient())
                {
                    var result = httpClient.GetAsync($"http://{s.Address}:{s.Port}/api/Values/1");


                    return $"调用{s.Service}，状态：{result.Result.StatusCode}，响应：{result.Result.Content.ReadAsStringAsync().Result}";
                }
            }

        }

        // GET api/values/redisconfig
        [HttpGet("{key}")]
        public ActionResult<string> Value(string key)
        {
            using (var consul = new Consul.ConsulClient(c =>
            {
                c.Address = new Uri("http://127.0.0.1:8500"); //Consul地址
            }))
            {
                var response = consul.KV.Get(key).Result.Response;
                if (response != null)
                {
                    return Encoding.UTF8.GetString(response.Value);
                }
                return "";
            }

        }
    }
}

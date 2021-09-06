using ConsoleLoja.Context;
using ConsoleLoja.Model;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLoja.Repository
{
    public class BaseRepo<M> where M : Loja
    {
        public void Create(M model)
        {
            using (var context = new LojaContext())
            {
                context.Set<M>().Add(model);
                context.SaveChanges();
            }
        }
    }
}


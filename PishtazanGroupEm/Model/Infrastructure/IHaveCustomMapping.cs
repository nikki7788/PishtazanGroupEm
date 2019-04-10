using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Infrastructure
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);

    }
}

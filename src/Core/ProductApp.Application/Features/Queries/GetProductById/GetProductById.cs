using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductApp.Application.Dto;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Application.Features.Queries.GetProductById
{
    public class GetProductById : IRequest<ServiceResponse<GetProductByIdViewModel>>
    {
        public Guid Id { get; set; }
        public class GetProductByIdHandler : IRequestHandler<GetProductById, ServiceResponse<GetProductByIdViewModel>>
        {
            private readonly IProductRepository productRepository;
            private readonly IMapper mapper;
            public GetProductByIdHandler(IProductRepository productRepository, IMapper mapper)
            {
                this.productRepository = productRepository;
                this.mapper = mapper;
            }
    
            public async Task<ServiceResponse<GetProductByIdViewModel>> Handle(GetProductById request, CancellationToken cancellationToken)
            {
                var product = await productRepository.GetByIdAsync(request.Id);
                var viewModel = mapper.Map<GetProductByIdViewModel>(product);
                return new ServiceResponse<GetProductByIdViewModel>(viewModel);
            }
        }
    }
}

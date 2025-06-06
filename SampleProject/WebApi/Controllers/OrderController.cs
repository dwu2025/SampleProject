using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Orders;
using Core.Services.Products;
using WebApi.Models.Orders;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IGetProductService _getProductService;

        public OrderController(ICreateOrderService createOrderService, IDeleteOrderService deleteOrderService, IGetOrderService getOrderService, IUpdateOrderService updateOrderService, IGetProductService getProductService)
        {
            _createOrderService = createOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderService = getOrderService;
            _getProductService = getProductService;
            _updateOrderService = updateOrderService;
        }

        [Route("{orderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var order = _getOrderService.GetOrder(orderId);

            if (order != null)
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, "error: this orderId already exists");


            OrderItem[] orderItemsArray = model.OrderItems.Select<OrderItemModel, OrderItem>(orderItem =>
            {
                var product = _getProductService.GetProduct(new Guid(orderItem.productId));
                if (product == null)
                {
                    throw new HttpResponseException(ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, "error: product does not exist"));
                }

                return new OrderItem
                {
                    Product = product,
                    Quantity = orderItem.quantity
                };
            }).ToArray();

            order = _createOrderService.Create(orderId, new Guid(model.CustomerId), orderItemsArray.ToList());
            
            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }

            OrderItem[] orderItemsArray = model.OrderItems.Select<OrderItemModel, OrderItem>(orderItem =>
            {
                var product = _getProductService.GetProduct(new Guid(orderItem.productId));
                if (product == null)
                {
                    throw new HttpResponseException(ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, "error: product does not exist"));
                }

                return new OrderItem
                {
                    Product = product,
                    Quantity = orderItem.quantity
                };
            }).ToArray();



            _updateOrderService.Update(order, order.CustomerId, orderItemsArray.ToList());
            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            _deleteOrderService.Delete(order);
            return Found();
        }

        [Route("{orderId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            return Found(new OrderData(order));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetOrders()
        {
            var orders = _getOrderService.GetOrders()
                                       .Select(q => new OrderData(q))
                                       .ToList();
            return Found(orders);
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetOrders(String searchProductName)
        {
            var orders = (from p in _getOrderService.GetOrders()
                          where p.OrderItems.Any(item => item.Product.Name.Contains(searchProductName))
                          select p).Select(q => new OrderData(q))
                                       .ToList();
            return Found(orders);
        }
    }
}
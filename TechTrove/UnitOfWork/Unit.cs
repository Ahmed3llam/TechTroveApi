using TechTrove.Models;
using TechTrove.Repository;

namespace TechTrove.UnitOfWork
{
    public class Unit
    {
        TechTroveContext db;

        Repository<Category> categoryRepo;
        Repository<Payment> paymentRepo;

        UserRepository userRepo;
        CartRepository cartRepo;
        CommentRepository commentRepo;
        OrderRepository orderRepo;
        OrderItemsRepository orderItemRepo;
        ProductRepository productRepo;
        PromoCodeRepository promoRepo;
        WishRepository wishRepo;

        public Unit(TechTroveContext db)
        {
            this.db = db;
        }
        public UserRepository UserRepo
        {
            get
            {
                if (userRepo == null)
                {
                    userRepo = new UserRepository(db);
                }
                return userRepo;
            }
        }
        public CartRepository CartRepo
        {
            get
            {
                if (cartRepo == null)
                {
                    cartRepo = new CartRepository(db);
                }
                return cartRepo;
            }
        }
        public Repository<Category> CategoryRepo
        {
            get
            {
                if (categoryRepo == null)
                {
                    categoryRepo = new Repository<Category>(db);
                }
                return categoryRepo;
            }
        }
        public CommentRepository CommentRepo
        {
            get
            {
                if (commentRepo == null)
                {
                    commentRepo = new CommentRepository(db);
                }
                return commentRepo;
            }
        }
        public OrderRepository OrderRepo
        {
            get
            {
                if (orderRepo == null)
                {
                    orderRepo = new OrderRepository(db);
                }
                return orderRepo;
            }
        }
        public OrderItemsRepository OrderItemRepo
        {
            get
            {
                if (orderItemRepo == null)
                {
                    orderItemRepo = new OrderItemsRepository(db);
                }
                return orderItemRepo;
            }
        }
        public ProductRepository ProductRepo
        {
            get
            {
                if (productRepo == null)
                {
                    productRepo = new ProductRepository(db);
                }
                return productRepo;
            }
        }
        public PromoCodeRepository PromoRepo
        {
            get
            {
                if (promoRepo == null)
                {
                    promoRepo = new PromoCodeRepository(db);
                }
                return promoRepo;
            }
        }
        public Repository<Payment> PaymentRepo
        {
            get
            {
                if (paymentRepo == null)
                {
                    paymentRepo = new Repository<Payment>(db);
                }
                return paymentRepo;
            }
        }
        public WishRepository WishRepo
        {
            get
            {
                if (wishRepo == null)
                {
                    wishRepo = new WishRepository(db);
                }
                return wishRepo;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}

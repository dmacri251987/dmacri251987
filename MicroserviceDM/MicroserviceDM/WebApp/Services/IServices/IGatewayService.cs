namespace WebApp.Services.IServices
{
    public interface IGatewayService
    {
        #region "Products - Categories"

        Task<T> GetProductCategoryByIdAsync<T>(int id);
        Task<T> GetProductsCategories<T>();

        #endregion
    }
}

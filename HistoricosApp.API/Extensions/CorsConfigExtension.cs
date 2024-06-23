namespace HistoricosApp.API.Extensions
{
    public static class CorsConfigExtension
    {
        private static string _policyName = "DefaultPolicy";

        public static IServiceCollection AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(s => s.AddPolicy(_policyName, builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            return services;
        }

        public static IApplicationBuilder UseCorsConfig(this IApplicationBuilder app)
        {
            app.UseCors(_policyName);
            return app;
        }
    }
}

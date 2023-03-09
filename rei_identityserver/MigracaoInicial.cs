using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace rei_identityserver;

public static class MigracaoInicial
{
    public static IHost CMX_MigrarBaseDeDados(this IHost p_host)
    {
        using var m_scope = p_host.Services.CreateScope();
        try
        {
            m_scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
        }
        catch (Exception) { }

        using var m_context = m_scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        try
        {
            m_context.Database.Migrate();

            var m_config_clients = InMemoryConfiguracao.CM_Clients().ToList();
            foreach(var client in m_config_clients)
            {
                if(m_context.Clients.Where(a => a.ClientId == client.ClientId).Count() == 0)
                {
                    m_context.Clients.Add(client.ToEntity());
                    m_context.SaveChanges();
                }
            }
            //if(m_context.Clients.Any() == false)
            //{
            //    foreach (var m_client in InMemoryConfiguracao.CM_Clients())
            //        m_context.Clients.Add(m_client.ToEntity());

            //    m_context.SaveChanges();
            //}

            var m_config_identity_resources = InMemoryConfiguracao.CM_IdentityResources().ToList();
            foreach(var identity_resource in m_config_identity_resources)
                if(m_context.IdentityResources.Where(a => a.Name == identity_resource.Name).Count() == 0)
                {
                    m_context.IdentityResources.Add(identity_resource.ToEntity());
                    m_context.SaveChanges();
                }
            //if(m_context.IdentityResources.Any() == false)
            //{
            //    foreach (var m_resource in InMemoryConfiguracao.CM_IdentityResources())
            //        m_context.IdentityResources.Add(m_resource.ToEntity());

            //    m_context.SaveChanges();
            //}

            var m_config_api_scopes = InMemoryConfiguracao.CM_ApiScopes().ToList();
            foreach(var api_scope in m_config_api_scopes)
                if(m_context.ApiScopes.Where(a => a.Name == api_scope.Name).Count() == 0)
                {
                    m_context.ApiScopes.Add(api_scope.ToEntity());
                    m_context.SaveChanges();
                }
            //if(m_context.ApiScopes.Any() == false)
            //{
            //    foreach (var m_resource in InMemoryConfiguracao.CM_ApiScopes())
            //        m_context.ApiScopes.Add(m_resource.ToEntity());

            //    m_context.SaveChanges();
            //}
            
            var m_config_api_resources = InMemoryConfiguracao.CM_ApiResources().ToList();
            foreach(var api_resource in m_config_api_resources)
                if(m_context.ApiResources.Where(a => a.Name == api_resource.Name).Count() == 0)
                {
                    m_context.ApiResources.Add(api_resource.ToEntity());
                    m_context.SaveChanges();
                }
            //if(m_context.ApiResources.Any() == false)
            //{
            //    foreach (var m_resource in InMemoryConfiguracao.CM_ApiResources())
            //        m_context.ApiResources.Add(m_resource.ToEntity());

            //    m_context.SaveChanges();
            //}
        }
        catch(Exception)
        {
        }

        return p_host;
    }
}

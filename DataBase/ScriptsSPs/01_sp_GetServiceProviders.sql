CREATE PROCEDURE [payments].[sp_GetServiceProviders]
    @ProviderId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        provider_id_ui,
        name_vc,
        adress_vc,
        nit_in,
        cell_phone_in,
        service_type_vc,
        email_vc,
        user_registration_vc,
        date_registration_dt,
        user_modification_vc,
        date_modification_dt,
        state_bt
    FROM payments.SERVICEPROVIDER
    WHERE
        provider_id_ui = @ProviderId
        AND state_bt = 1
    ORDER BY date_registration_dt DESC;
END
GO
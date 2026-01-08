CREATE PROCEDURE [payments].[sp_RegisterServiceProvider]
    @Name VARCHAR(50),
    @Address VARCHAR(100) = NULL,
    @Nit INT,
    @CellPhone INT = NULL,
    @ServiceType VARCHAR(20),
    @Email VARCHAR(50) = NULL,
    @UserRegistration VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ProviderId UNIQUEIDENTIFIER;
    SET @ProviderId = NEWID();

    INSERT INTO payments.SERVICEPROVIDER
    (
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
    )
    VALUES
    (
        @ProviderId,
        @Name,
        @Address,
        @Nit,
        @CellPhone,
        @ServiceType,
        @Email,
        @UserRegistration,
        GETDATE(),
        @UserRegistration,
        GETDATE(),
        1
    );

    SELECT @ProviderId;
END
GO
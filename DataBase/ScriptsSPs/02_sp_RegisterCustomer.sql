CREATE PROCEDURE [payments].[sp_RegisterCustomer]
    @Name VARCHAR(60),
    @LastName VARCHAR(50),
    @IdentificationNumber INT,
    @IdentificationExtension CHAR(2),
    @IdentificationComplement CHAR(2),
    @BirthDate DATE = NULL,
    @CellPhone INT = NULL,
    @Email VARCHAR(50) = NULL,
    @UserRegistration VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CustomerId UNIQUEIDENTIFIER;
    SET @CustomerId = NEWID();

    INSERT INTO payments.CUSTOMER
    (
        customer_id_ui,
        name_vc,
        last_name_vc,
        identification_number_in,
        identification_extension_ch,
        identification_complement_ch,
        birth_date_dt,
        cell_phone_in,
        email_vc,
        user_registration_vc,
        date_registration_dt,
        user_modification_vc,
        date_modification_dt,
        state_bt
    )
    VALUES
    (
        @CustomerId,
        @Name,
        @LastName,
        @IdentificationNumber,
        @IdentificationExtension,
        @IdentificationComplement,
        @BirthDate,
        @CellPhone,
        @Email,
        @UserRegistration,
        GETDATE(),
        @UserRegistration,
        GETDATE(),
        1
    );

    SELECT @CustomerId;
END
GO
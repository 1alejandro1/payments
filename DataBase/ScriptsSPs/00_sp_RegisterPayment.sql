CREATE PROCEDURE [payments].[sp_RegisterPayment]
    @CustomerId UNIQUEIDENTIFIER,
    @ProviderId UNIQUEIDENTIFIER,
    @Amount DECIMAL(18,2),
    @CurrencyType CHAR(3),
    @Status VARCHAR(20),
    @UserRegistration VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @PaymentId UNIQUEIDENTIFIER;
        SET @PaymentId = NEWID();

        INSERT INTO [payments].[PAYMENT]
        (
            [payment_id_ui],
            [customer_id_ui],
            [provider_id_ui],
            [amount_dc],
            [currency_type_ch],
            [status_vc],
            [user_registration_vc],
            [date_registration_dt],
            [user_modification_vc],
            [date_modification_dt],
            [state_bt]
        )
        VALUES
        (
            @PaymentId,
            @CustomerId,
            @ProviderId,
            @Amount,
            @CurrencyType,
            @Status,
            @UserRegistration,
            GETDATE(),
            @UserRegistration,
            GETDATE(),
            1
        );
		SELECT @PaymentId;
END
GO
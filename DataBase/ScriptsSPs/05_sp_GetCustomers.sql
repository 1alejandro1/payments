CREATE PROCEDURE [payments].[sp_GetCustomers]
    @CustomerId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
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
    FROM payments.CUSTOMER
    WHERE
        (customer_id_ui = @CustomerId)
        AND (state_bt = 1)
    ORDER BY date_registration_dt DESC;
END
GO
using System.Net;
using System.Net.NetworkInformation;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ProductNotFound => new(HttpStatusCode.NotFound, "Product not found!", ErrorCodes.EntityNotFound);
    public static ErrorMessage OrderNotFound => new(HttpStatusCode.NotFound, "Order not found!", ErrorCodes.EntityNotFound);
    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
    public static ErrorMessage UserAlreadyExists => new(HttpStatusCode.Conflict, "User already exists!", ErrorCodes.UserAlreadyExists);
    public static ErrorMessage WrongPassword => new(HttpStatusCode.BadRequest, "Wrong password!", ErrorCodes.WrongPassword);
    public static ErrorMessage CannotAddUsers => new(HttpStatusCode.Forbidden, "Only the admin can add users!", ErrorCodes.CannotAdd);
    public static ErrorMessage CannotAddOrder => new(HttpStatusCode.Forbidden, "A user cannot order his products!", ErrorCodes.CannotAdd);
    public static ErrorMessage CannotDeleteOrder => new(HttpStatusCode.Forbidden, "Only the owner of the product or the user who placed the order can remove it!", ErrorCodes.CannotDelete);
    public static ErrorMessage CannotUpdateUsers => new(HttpStatusCode.Forbidden, "Only the admin or the own user can update the user!", ErrorCodes.CannotUpdate);
    public static ErrorMessage CannotDeleteUsers => new(HttpStatusCode.Forbidden, "Only the admin or the own user can delete the user!", ErrorCodes.CannotDelete);
    public static ErrorMessage CannotAddProductsTags => new(HttpStatusCode.Forbidden, "Only the admin or personnel can add product tags!", ErrorCodes.CannotAdd);
    public static ErrorMessage CannotDeleteProductsTags => new(HttpStatusCode.Forbidden, "Only the admin or personnel can delete product tags!", ErrorCodes.CannotDelete);
    public static ErrorMessage CannotDeleteProducts => new(HttpStatusCode.Unauthorized, "A product can be deleted olny by owner or admin/personnel!", ErrorCodes.CannotDelete);
    public static ErrorMessage WrongInputs => new(HttpStatusCode.BadRequest, "Every input should have at least 1 character!", ErrorCodes.WrongInputs);
    public static ErrorMessage WrongInputsProduct => new(HttpStatusCode.BadRequest, "Every input should have at least 1 character and the price should be higher then 0!", ErrorCodes.WrongInputs);
    public static ErrorMessage TagAlreadyExists => new(HttpStatusCode.Conflict, "Tag already exists!", ErrorCodes.TagAlreadyExists);
    public static ErrorMessage WrongTag => new(HttpStatusCode.BadRequest, "The tag should have at least 1 character!", ErrorCodes.WrongTag);
    public static ErrorMessage MailSendFailed => new(HttpStatusCode.ServiceUnavailable, "Mail couldn't be send!", ErrorCodes.MailSendFailed);
    public static ErrorMessage OrderAlreadyExists => new(HttpStatusCode.Conflict, "Order already exists!", ErrorCodes.OrderAlreadyExists);


}

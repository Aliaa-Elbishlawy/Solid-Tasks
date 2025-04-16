// Apply SOLID design principles on the following code samples for better design
//1.  
using static Circle;
using static Rectangle;

public class UserService  
{  
   public void Register(string email, string password)  
   {  
      if (!ValidateEmail(email))  
         throw new ValidationException("Email is not an email");  
      
      var user = new User(email, password);  
  
      SendEmail(new MailMessage("mysite@nowhere.com", email) { Subject="HEllo foo" });  
   }
   public virtual bool ValidateEmail(string email)  
   {  
     return email.Contains("@");  
   }
   public bool SendEmail(MailMessage message)
   {
       _smtpClient.Send(message);
   }

}   
 -----------------------------------------------------
    #region solution 1
public interface IEmailService
{
    public bool SendEmail(MailMessage message)
}
public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    public EmailService(SmtpClient smtpClient)
    {
        _smtpClient = smtpClient;
    }
    public bool SendEmail(MailMessage message)
    {
        _smtpClient.Send(message);
    }
}
public interface IvalidationService
{
    public bool ValidateEmail(string email)
}
public class ValidationService : IvalidationService
{
    public bool ValidateEmail(string email)
    {
        return email.Contains("@");
    }
}

public class UserService  
{
    private readonly IEmailService _emailService;
    private readonly IvalidationService _validationService;
    public UserService(IEmailService emailService, IvalidationService validationService)
    {
        _emailService = emailService;
        _validationService = validationService;
    }
    if (!_validationService.ValidateEmail(email))  
         throw new ValidationException("Email is not an email");

    var user = new User(email, password);
    _emailService.SendEmail(new MailMessage("mysite@nowhere.com", email) { Subject = "HEllo foo" });  
}

#endregion

// 2.
//a. Add Square & Triangle & Cube
//b. Add function to get volume for the supported shapes
//c. noting that cube shape only support volume calculation
public class Rectangle
{
    public double Height { get; set; }
    public double Width { get; set; }
}
public class Circle
{
    public double Radius { get; set; } 
}
public class AreaCalculator
{
    public double TotalArea(object[] arrObjects)
    {
        double area = 0;
        Rectangle objRectangle;
        Circle objCircle;
        foreach (var obj in arrObjects)
        {
            if (obj is Rectangle)
            {
                area += obj.Height * obj.Width;
            }
            else
            {
                objCircle = (Circle)obj;
                area += objCircle.Radius * objCircle.Radius * Math.PI;
            }
        }
        return area;
    }
}
#region solution 2
public interface IVolumeCalculator
{
    double CalculateVolume();
}
public interface IAreaCalculator
{
    double CalculateArea();
}
public class Rectangle : IAreaCalculator, IVolumeCalculator
{
    public double Height { get; set; }
    public double Width { get; set; }
    public double CalculateArea()
    {
        return Height * Width;
    }
    public double CalculateVolume()
    {
        return Height * Width * 1; // Assuming depth of 1 for volume calculation
    }
}
public class Circle : IAreaCalculator
{
    public double Radius { get; set; }
    public double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}
\public class Square : IAreaCalculator, IVolumeCalculator
{
    public double Side { get; set; }
    public double CalculateArea()
    {
        return Side * Side;
    }
    public double CalculateVolume()
    {
        return Side * Side * Side; // Volume for cube
    }
}
public class Triangle : IAreaCalculator
{
    public double Base { get; set; }
    public double Height { get; set; }
    public double CalculateArea()
    {
        return 0.5 * Base * Height;
    }
}
public class Cube : IVolumeCalculator
{
    public double Side { get; set; }
    public double CalculateVolume()
    {
        return Side * Side * Side; // Volume for cube
    }
}
#endregion  

// 3.
class Rectangle
 def initialize(width, height)
 @width, @height = width, height
 end
 def set_width(width)
 @width = width
 end
 def set_height(height)
 @height = height
 end
end
class Square<Rectangle "inherits"
 def set_width(width)
 super(width)
 @height = height
 end
 def set_height(height)
 super(height)
 @width = width
 end
end

#region solution 3
public interface IShape
{
    double setWidth();
    double setHeight();
}
public class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }
    public void setWidth(double width)
    {
        Width = width;
    }
    public void setHeight(double height)
    {
        Height = height;
    }
}
public class Square : IShape
{
    public double side { get; set; }
    public void setWidth(double width)
    {
        side = width;
    }
    public void setHeight(double height)
    {
        side = height;
    }
}
public interface OneDimentionalShape
{
    public void setSide(double side)
}
public class square : OneDimentionalShape
{
    public double Side { get; set; }
    public void setSide(double side)
    {
        this.Side = side;
    }
}
public class TwoDimentionalShape 
{
    public double Width { get; set; }
    public double Height { get; set; }
    public void setWidth(double width)
    {
        Width = width;
        Height = 0;
    }
    public void setHeight(double height)
    {
        Height = height;
        Width = 0;
    }
}
public class Rectangle : TwoDimentionalShape
{
    public double Width { get; set; }
    public double Height { get; set; }
    public void setWidth(double width)
    {
        Width = width;
    }
    public void setHeight(double height)
    {
        Height = height;
    }
}
#endregion
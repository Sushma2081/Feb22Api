using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Feb22Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly Feb22ApiContext _context;


    public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                     Feb22ApiContext context)
    {
        _logger = logger;
        _context = context;
    }


    [HttpGet(Name ="GetStudentsdata")]
    public async Task<IEnumerable<Student>> StudentsFetch()
    {
        return await _context.Students.ToListAsync();
    }

    [HttpGet(Name = "GetSubjectsdata")]
    public async Task<IEnumerable<Subject>> SubjectsFetch()
    {
        return await _context.Subjects.ToListAsync();
    }

    [HttpGet(Name ="getmarks")]
    public async Task<IEnumerable<Marks>> MarksFetch()
    {
        return await _context.Marks.ToListAsync();
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<object>>> GetMarksData()
    {
        var result = await _context.Marks
            .Join(_context.Students,
                m => m.StudentId,
                s => s.StudentId,
                (m, s) => new { StudentName = s.StudentName, m.SubjectId, m.marks })
            .Join(_context.Subjects,
                m => m.SubjectId,
                sb => sb.SubjecttId,
                (m, sb) => new { m.StudentName, SubjectName = sb.SubjectName, m.marks })
            .ToListAsync();
        Console.WriteLine(result);
        return Ok(result);
    }



    [HttpPost(Name ="Addstudent")]
    public async Task<IActionResult> AddStudent(Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }
    [HttpPost(Name = "Addsubject")]
    public async Task<IActionResult> AddSubject(Subject subject)
    {
        if (ModelState.IsValid)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return Ok();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostMarks(Marks marks)
    {

        var existingStudent = await _context.Students.FindAsync(marks.StudentId);
        var existingSubject = await _context.Subjects.FindAsync(marks.SubjectId);

        if (existingStudent == null || existingSubject == null)
        {
            return NotFound();
        }

        
        marks.Student = existingStudent;
        marks.Subject = existingSubject;
        _context.Marks.Add(marks);
        await _context.SaveChangesAsync();

        return Ok(CreatedAtAction("GetMarks", new { StudentId = marks.StudentId, SubjectId = marks.SubjectId }, marks));

    }

    [HttpPut(Name = "Updatestudent")]
    public async Task<IActionResult> UpdateStudent(Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return Ok();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpPut(Name ="Updatesubject")]
    public async Task<IActionResult> UpdateSubject(Subject subject)
    {
        if (ModelState.IsValid)
        {
            _context.Subjects.Update(subject); 
            await _context.SaveChangesAsync();
            return Ok();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpPut(Name = "Updatemarks")]
    public async Task<IActionResult> Updatemarks(Marks marks)
    {
        /*var existingStudent = await _context.Students.FindAsync(marks.StudentId);
        var existingSubject = await _context.Subjects.FindAsync(marks.SubjectId);

        if (existingStudent == null || existingSubject == null)
        {
            return NotFound();
        }*/

        var existingmarks = await _context.Marks.Where(X=>X.StudentId==marks.StudentId && X.SubjectId==marks.SubjectId).FirstOrDefaultAsync();
        existingmarks.marks = marks.marks;
        
        _context.Marks.Update(marks);
        await _context.SaveChangesAsync();

        return Ok();
    }



    [HttpDelete(Name = "deletestudent")]
    public async Task<IActionResult> Deletestudent(int Id)
    {
        var recordtobedel = _context.Students.Find(Id);
        if (recordtobedel == null)
        {
            return NotFound();
        }
        else
        {
            _context.Students.Remove(recordtobedel);
            _context.SaveChanges();
            return Ok();
        }
    }
    [HttpDelete(Name = "deletesubject")]
    public async Task<IActionResult> Deletesubject(int Id)
    {
        var recordtobedel = _context.Subjects.Find(Id);
        if (recordtobedel == null)
        {
            return NotFound();
        }
        else
        {
            _context.Subjects.Remove(recordtobedel);
            _context.SaveChanges();
            return Ok();
        }
    }

    [HttpDelete(Name = "deletemarks")]
    public async Task<IActionResult> Deletemarks(int StudentId,int SubjectId)
    {
        var recordtobedel = _context.Marks.Find(StudentId,SubjectId);
        if (recordtobedel == null)
        {
            return NotFound();
        }
        else
        {
            _context.Marks.Remove(recordtobedel);
            _context.SaveChanges();
            return Ok();
        }
    }




}

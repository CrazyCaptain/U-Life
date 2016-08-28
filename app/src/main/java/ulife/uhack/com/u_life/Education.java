package ulife.uhack.com.u_life;

import android.app.Activity;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import org.joda.time.DateTime;
import org.joda.time.Months;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.text.SimpleDateFormat;
import java.text.StringCharacterIterator;
import java.util.Calendar;
import java.util.Date;
import java.util.concurrent.TimeUnit;

public class Education extends Activity
{

    TextView txtComputation;
    Spinner spnMonth, spnYear, spnCollege;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_education);


        txtComputation = (TextView) findViewById(R.id.txtComputation);

        spnMonth = (Spinner) findViewById(R.id.spnMonth);
        ArrayAdapter<String> adapterMonth = new ArrayAdapter<String>(this, R.layout.spinner_item, getResources().getStringArray(R.array.month_arrays));
        spnMonth.setAdapter(adapterMonth);


        spnYear = (Spinner) findViewById(R.id.spnYear);
        ArrayAdapter<String> adapterYear = new ArrayAdapter<String>(this, R.layout.spinner_item, getResources().getStringArray(R.array.years_arrays));
        spnYear.setAdapter(adapterYear);

        spnCollege = (Spinner) findViewById(R.id.spnCollege);
        ArrayAdapter<String> adapterCollege = new ArrayAdapter<String>(this, R.layout.spinner_item, getResources().getStringArray(R.array.colleges_arrays));
        spnCollege.setAdapter(adapterCollege);
    }

    public void doComputation(View v)
    {
        try
        {

            String month = spnMonth.getSelectedItem().toString();
            String year = spnYear.getSelectedItem().toString();

            Calendar calendar = Calendar.getInstance();
            int day = calendar.get(Calendar.DAY_OF_MONTH);

            String strDateOfCollege = month + " " + Integer.toString(day) + ", " + year;

            SimpleDateFormat format = new SimpleDateFormat("MMMMM d, yyyy");
            Date dateOfCollege = format.parse(strDateOfCollege);

            Date dateToday = calendar.getTime();


            DateTime dateTimeNow = new DateTime(dateToday);
            DateTime dateTimeCollege = new DateTime(dateOfCollege);


            int monthBefore = Months.monthsBetween(dateTimeNow, dateTimeCollege).getMonths();

            //Toast.makeText(this, Integer.toString(monthBefore.getMonths()), Toast.LENGTH_SHORT).show();

            double yearlyCost = 210000;
            double totalCost = 210000 * 4;

            double monthlyCost = totalCost / monthBefore;


            txtComputation.setText("Your monthly cost is Php " + Double.toString(round(monthlyCost, 2)) + " for " + monthBefore + " months.");



        }
        catch (Exception ex)
        {
            Log.d("err", ex.getMessage());
        }
    }

    public static double round(double value, int places) {
        if (places < 0) throw new IllegalArgumentException();

        BigDecimal bd = new BigDecimal(value);
        bd = bd.setScale(places, RoundingMode.HALF_UP);
        return bd.doubleValue();
    }



}

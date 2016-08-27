package ulife.uhack.com.u_life;

import android.app.Activity;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.Spinner;

public class Education extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_education);


        Spinner spnMonth = (Spinner) findViewById(R.id.spnMonth);
        ArrayAdapter<String> adapterMonth = new ArrayAdapter<String>(this, R.layout.spinner_item, getResources().getStringArray(R.array.month_arrays));
        spnMonth.setAdapter(adapterMonth);


        Spinner spnYear = (Spinner) findViewById(R.id.spnYear);
        ArrayAdapter<String> adapterYear = new ArrayAdapter<String>(this, R.layout.spinner_item, getResources().getStringArray(R.array.years_arrays));
        spnYear.setAdapter(adapterYear);
    }
}

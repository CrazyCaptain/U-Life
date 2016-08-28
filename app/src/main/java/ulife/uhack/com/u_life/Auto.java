package ulife.uhack.com.u_life;

import android.app.Activity;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.Spinner;

public class Auto extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_auto);

        Spinner spnBrand = (Spinner) findViewById(R.id.spnBrand);
        ArrayAdapter<String> adapteBrand = new ArrayAdapter<String>(this, R.layout.spinner_item, getResources().getStringArray(R.array.carbrand_arrays));
        spnBrand.setAdapter(adapteBrand);

        Spinner spnAutoYear = (Spinner) findViewById(R.id.spnAutoYear);
        ArrayAdapter<String> adapterAutoYear = new ArrayAdapter<String>(this, R.layout.spinner_item, getResources().getStringArray(R.array.years_arrays_auto));
        spnAutoYear.setAdapter(adapterAutoYear);

    }
}

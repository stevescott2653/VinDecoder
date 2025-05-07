namespace VinDecoderSpike;

public class VinValidator
{
    public bool ValidateVin(string vin)
    {
        // A simple VIN validation might check the length and the characters
        if (vin.Length != 17) return false;

        foreach (var c in vin)
        {
            if (!char.IsLetterOrDigit(c)) return false;

            // VINs should not contain the letters 'I', 'O', or 'Q'
            if (c == 'I' || c == 'O' || c == 'Q') return false;
        }

        // If the VIN passes all checks, return true
        return true;
    }
}
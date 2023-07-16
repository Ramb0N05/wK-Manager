namespace wK_Manager.Base.Extensions {

    public static class SizeExtensions {

        public static Size GetWithAspectRatio(this Size size) => GetWithAspectRatio(size, 1, 1);

        public static Size GetWithAspectRatio(this Size size, int ratioX, int ratioY) {
            double aspectRatio = Math.Round((double)(ratioX / ratioY));
            double targetArea = size.Width * size.Height;

            double newWidth = Math.Sqrt(aspectRatio * targetArea);
            double newHeight = targetArea / newWidth;

            return new((int)Math.Round(newWidth), (int)Math.Round(newHeight));
        }
    }
}

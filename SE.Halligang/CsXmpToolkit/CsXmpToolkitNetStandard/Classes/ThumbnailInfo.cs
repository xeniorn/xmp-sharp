using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SE.Halligang.CsXmpToolkit
{
	/// <summary>
	/// 
	/// </summary>
	public class ThumbnailInfo
	{
		internal ThumbnailInfo()
		{
		}

		private FileFormat fileFormat = FileFormat.Unknown;
		/// <summary>
		/// The format of the containing file.
		/// </summary>
		public FileFormat FileFormat
		{
			get { return fileFormat; }
			internal set { fileFormat = value; }
		}

		private int fullWidth = 0;
		/// <summary>
		/// Full image width in pixels.
		/// </summary>
		public int FullWidth
		{
			get { return fullWidth; }
			internal set { fullWidth = value; }
		}

		private int fullHeight = 0;
		/// <summary>
		/// Full image height in pixels.
		/// </summary>
		public int FullHeight
		{
			get { return fullHeight; }
			internal set { fullHeight = value; }
		}

		private int thumbnailWidth = 0;
		/// <summary>
		/// Thumbnail image width in pixels.
		/// </summary>
		public int ThumbnailWidth
		{
			get { return thumbnailWidth; }
			internal set { thumbnailWidth = value; }
		}

		private int thumbnailHeight = 0;
		/// <summary>
		/// Thumbnail image height in pixels.
		/// </summary>
		public int ThumbnailHeight
		{
			get { return thumbnailHeight; }
			internal set { thumbnailHeight = value; }
		}

		private short fullOrientation = 0;
		/// <summary>
		/// Orientation of full image, as defined by Exif for tag 274.
		/// </summary>
		public short FullOrientation
		{
			get { return fullOrientation; }
			internal set { fullOrientation = value; }
		}

		private short thumbnailOrientation = 0;
		/// <summary>
		/// Orientation of thumbnail, as defined by Exif for tag 274.
		/// </summary>
		public short ThumbnailOrientation
		{
			get { return thumbnailOrientation; }
			internal set { thumbnailOrientation = value; }
		}

		private IntPtr thumbnailImage = IntPtr.Zero;
		/// <summary>
		/// Raw data from the host file, valid for life of the owning XMPFiles object. Do not modify!
		/// </summary>
		internal IntPtr ThumbnailImage
		{
			get { return thumbnailImage; }
			set { thumbnailImage = value; }
		}

		private int thumbnailSize = 0;
		/// <summary>
		/// The size in bytes of the tnailImage data.
		/// </summary>
		public int ThumbnailSize
		{
			get { return thumbnailSize; }
			internal set { thumbnailSize = value; }
		}

		private ThumbnailFormat thumbnailFormat = ThumbnailFormat.Unknown;
		/// <summary>
		/// The format of the tnailImage data.
		/// </summary>
		public ThumbnailFormat ThumbnailFormat
		{
			get { return thumbnailFormat; }
			internal set { thumbnailFormat = value; }
		}

		/// <summary>
		/// Gets the thumbnails as a bitmap bytes
		/// </summary>
		/// <returns>The thumbnail from the file, or empty array if it could not be read/parsed.</returns>
		public byte[] GetThumbnail()
		{
			if (thumbnailImage != IntPtr.Zero && 
				thumbnailSize > 0)
			{
				try
				{
					byte[] thumbnailBuffer = new byte[thumbnailSize];
					Marshal.Copy(thumbnailImage, thumbnailBuffer, 0, thumbnailBuffer.Length);
                    return thumbnailBuffer;
				}
                catch
                {
                    return Array.Empty<byte>();
                }
            }
			else
			{
				return Array.Empty<byte>();
			}
		}
	}
}

﻿namespace Battleship.Core.Components.Board
{
    using System.Collections.Generic;

    using Battleship.Core.Models;

    /// <summary>
    /// List of segments Controls each individual segment on the grid.
    /// </summary>
    public interface ISegmentation
    {
        /// <summary>
        /// Add a segment to the segmentation grid
        /// </summary>
        /// <param name="segment">Segment of the grid</param>
        void AddSegment(Segment segment);

        /// <summary>
        /// Update a segment in the segmentation grid
        /// </summary>
        /// <param name="segment">A segment within the grid</param>
        void UpdateSegment(Segment segment);

        /// <summary>
        /// Update a range of segments in the segmentation grid
        /// </summary>
        /// <param name="segment">A list of segment within the grid</param>
        void UpdateSegmentRange(IList<Segment> segment);

        /// <summary>
        ///     Get the list of segments in the segmentation list
        /// </summary>
        /// <returns>A list of Segments</returns>
        IList<Segment> GetSegments();

        /// <summary>
        ///     Gets a single segment
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>Returns a single segment</returns>
        Segment GetSegment(int x, int y);
    }
}
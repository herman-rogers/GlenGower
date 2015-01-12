//-----------------------------------------------------------------------
// <copyright company="Periscope Studio">
//     Copyright (c) Periscope Studio UG & Co. KG. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace psai.net
{

    // this is used by PlaybackChannel as an interface to directly inform some
    // AudioLayer to load and play back Snippets.
    internal interface IAudioPlaybackLayerChannel
    {         
        PsaiResult LoadSnippet(Segment snippet);
        PsaiResult ReleaseSnippet();
        PsaiResult ScheduleSnippetPlayback(Segment snippet, int delayMilliseconds);
        PsaiResult StopChannel();
        PsaiResult SetVolume(float volume);
        PsaiResult SetPaused(bool paused);
    }
}

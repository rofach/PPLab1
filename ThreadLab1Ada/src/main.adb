with Ada.Text_IO; use Ada.Text_IO;
with Ada.Integer_Text_IO; use Ada.Integer_Text_IO;
with Ada.Real_Time; use Ada.Real_Time;

procedure Main is
   threads_count : Integer := 5;
   type int_array is array (1 .. threads_count) of Integer;
   type stop_flags_array is array (1 .. threads_count) of Boolean;
   
   pragma Atomic_Components (stop_flags_array);

   steps, duration : int_array;
   flags : stop_flags_array := (others => False);
   task type main_thread is
      entry Run (ID : in Integer; Step : in Integer;
                 flags : in stop_flags_array);
   end main_thread;
   task body main_thread is
      local_id : Integer;
      local_step : Integer;
      sum   : Long_Long_Integer := 0;
      count : Long_Long_Integer := 0;
      curr  : Long_Long_Integer := 0;
      start_time : Time;
   begin
      accept Run (ID : in Integer; Step : in Integer; flags : in stop_flags_array) do
         local_id := ID;
         local_step := Step;
      end Run;

      start_time := Clock;
      Put_Line ("Thread " & Integer'Image (local_id) & " is running with step " & Integer'Image (local_step));

      while not flags(local_id) loop
         sum:= sum + curr;
         count := count + 1;
         curr := curr + Long_Long_Integer(local_step);
      end loop;
      Put_Line (
         "Thread " & Integer'Image (local_id) 
         & " has finished. Sum: " 
         & Long_Long_Integer'Image (sum) & ", Count: " 
         & Long_Long_Integer'Image (count)
         & ", Elapsed Time: "
         & Time_Span'Image (Clock - start_time));
   end main_thread;

task stopper is
      entry Run (dur : in int_array);
   end stopper;
   task body stopper is
      start_time : Time;
      elapsed : Time_Span;
      done : Boolean := False;
      internal_dur : int_array;
      begin
         accept Run (dur : in int_array) do
            internal_dur := dur;
         end Run;
         start_time := Clock; 
         
         loop
            elapsed := Clock - start_time;
            done := true;
            for i in 1 .. threads_count loop
               if not flags(i) then
                  if elapsed >= Milliseconds (internal_dur(i)) then
                     flags(i) := True;
                  else
                     done := False;
                  end if;
               end if;
            end loop;
            exit when done;
         end loop;  
   end stopper;
begin
   Put_Line ("Enter the number of threads for calculation:");
   Get (threads_count);  
   Put_Line ("Number of threads: " & Integer'Image (threads_count));

begin
   Put_Line ("Enter working time for each thread");
   for i in 1 .. threads_count loop
      Get (duration (i));
   end loop;
   Put_Line ("Enter steps for each thread:");
   for i in 1 .. threads_count loop
      Get (steps (i));
   end loop;

   for i in 1 .. threads_count loop
      Put_Line ("Thread " & Integer'Image (i) & ": " &
                "Working time = " & Integer'Image (duration (i)) & " ms, " &
                "Steps = " & Integer'Image (steps (i)));
   end loop;

declare
   
threads_pool : array (1 .. threads_count) of main_thread;

begin
   for i in 1 .. threads_count loop
      threads_pool(i).Run (ID => i, Step => steps (i), flags => flags);
   end loop;
   Put_Line ("All threads have been started.");
   stopper.Run (dur => duration);
   end;
end;
end Main;